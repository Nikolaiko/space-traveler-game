using System.Collections;
using UnityEngine;
using Zenject;

public class FuelScene : MonoBehaviour, CoroutineScope, BypassResultListener
{
    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private FuelSceneUI sceneUI;

    [Inject]
    private TipsManager tipsManager;

    [Inject]
    private LocalDataManager localDataManager;

    public Camera mainCamera;
    public FuelSceneCharacterController characterController;
    public FuelStoreDoorType lastOpenedDoorType = FuelStoreDoorType.normal;
    public GameObject sceneVisual;

    private float timeLeft = FuelSceneConsts.TIME_FOR_FUEL_LEVEL;
    private int totalFuel = 0;
    private int fuelNeeded = 0;
    private FuelStoreDoor lastOpenedDoor;
    private bool levelFinished = false;

    public void Start() {
        SpaceShipState? shipState = localDataManager.getSavedState();
        if (!shipState.HasValue || !shipState.Value.fuelNeeded.HasValue) {
            Debug.LogWarning("FuelScene: no saved fuel goal, returning to game progress");
            levelFinished = true;
            fuelGameEnded();
            return;
        }

        fuelNeeded = shipState.Value.fuelNeeded.Value;
        sceneUI.updateNeededFuel(fuelNeeded);
        sceneUI.onTipScreenClosed = onTipClosed;
        sceneUI.onWinScreenClose = fuelGameEnded;
        sceneUI.onLooseScreenClose = fuelGameEnded;

        characterController.onDoorOpened += onDoorOpen;

        if (!tipsManager.tipWasShown(GameTipType.fuelCollectionTip)) {            
            sceneUI.showTip(GameTipType.fuelCollectionTip);
        }  
    }

    public void Update() {
        if (levelFinished) { return; }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0) {
            finishLevel();
        } else {
            sceneUI.updateTimeLeft(timeLeft);            
        }
        sceneUI.updateCollectedFuel(totalFuel);
    }

    public void addCollectedFuel(int collectedValue) {
        totalFuel += collectedValue;
    }

    public void destroyLastCollidedDoor() {
        sceneLoader.unloadScene(SceneNumbers.FUEL_SOCOBAN_SCENE_ID, onRegularSocobanUnload, this);
    }

    #region BypassResultListener

    public void onResult(bool success) {
        if (success) {
            sceneLoader.unloadScene(SceneNumbers.BYPASS_SCHEME_PAIRS_SCENE_ID,
                                onSuccessBypassUnloaded,
                                this);
        } else {
            sceneLoader.unloadScene(SceneNumbers.BYPASS_SCHEME_PAIRS_SCENE_ID,
                                onRegularSocobanUnload,
                                this);
        }
    }

    #endregion

    #region CoroutineScope Methods
    public void launch(IEnumerator routine) {
        StartCoroutine(routine);
    }
    #endregion

    private void onRegularSocobanUnload() {
        showMainFuelLevel();

        lastOpenedDoor.destroyDoor();
        lastOpenedDoor = null;

        if (!haveDoorsLeft() || totalFuel >= fuelNeeded) {
            finishLevel();
        }
    }

    private void onSuccessBypassUnloaded() {
        sceneLoader.loadSceneAsyncAdditive(SceneNumbers.FUEL_SOCOBAN_SCENE_ID, onSocobanLoaded, this); 
    }

    private void onDoorOpen(FuelStoreDoor door) {
        lastOpenedDoor = door;
        lastOpenedDoorType = door.doorType;

        sceneLoader.loadSceneAsyncAdditive(SceneNumbers.BYPASS_SCHEME_PAIRS_SCENE_ID, onBypassLoaded, this);
    }

    private void onBypassLoaded() {
        hideMainFuelLevel();
    }

    private void onSocobanLoaded() {
        hideMainFuelLevel();
    }

    private void hideMainFuelLevel() {
        sceneUI.hideUI();

        characterController.deactivate();
        mainCamera.gameObject.SetActive(false);
        sceneVisual.gameObject.SetActive(false);
    }

    private void showMainFuelLevel() {
        sceneVisual.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(true);
        characterController.activate();

        sceneUI.showUI();
    }

    private void onTipClosed(GameTipType tipType) {
        tipsManager.setTipWasShown(tipType, true);
    }

    private void finishLevel() {
        if (levelFinished) { return; }
        levelFinished = true;

        if (totalFuel >= fuelNeeded)
        {
            SpaceShipState? shipState = localDataManager.getSavedState();
            if (shipState.HasValue) {
                SpaceShipState newState = shipState.Value.copy(
                    fuelCollected: totalFuel
                );
                localDataManager.saveGameState(newState);
            } 
            sceneUI.showWinScreen();
        } else
        {
            sceneUI.showLooseScreen();
        }
    }

    private void fuelGameEnded()
    {
        sceneLoader.loadScene(GameSceneType.gameProgress);
    }

    private bool haveDoorsLeft() {
        return FindObjectsByType<FuelStoreDoor>(FindObjectsSortMode.None).Length > 0;
    }
}
