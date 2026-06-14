using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SocobanScene : MonoBehaviour
{
    [Inject]
    private SocobanLevelBuilder levelBuilder;

    [Inject]
    private TipsManager tipsManager;

    [Inject]
    private SocobanLevelUI socobanLevelUI;

    private FuelScene fuelScene;
    private SocobanPlayer player;
    private SocobanBox[] boxes = Array.Empty<SocobanBox>();
    private bool readyForInput;

    public void Start() {
        socobanLevelUI.onTipScreenClosed += onTipClosed;

        fuelScene = GameObject.FindFirstObjectByType<FuelScene>();

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(OlgaSceneNumbers.FUEL_SOCOBAN_SCENE_ID));
        FuelStoreDoorType lastDoorType = fuelScene == null ? FuelStoreDoorType.normal : fuelScene.lastOpenedDoorType;

        levelBuilder.buildLevel(lastDoorType);
        player = FindFirstObjectByType<SocobanPlayer>();

        boxes = FindObjectsByType<SocobanBox>(FindObjectsSortMode.None);
        socobanLevelUI.setTotalBoxCount(boxes.Length);

        foreach (var box in boxes) {
            box.onBoxArrived += onBoxArrived;
        }

        if (!tipsManager.tipWasShown(GameTipType.socobanTip)) {
            socobanLevelUI.showTip(GameTipType.socobanTip);
        }        
    }

    public void Update() {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementInput.Normalize();

        if (player) {
            if (IsLevelComplete()) {                
                foreach(var box in boxes) {
                    int value = box.boxType == SocobanBoxType.usualBox
                        ? FuelSceneConsts.NORMAL_BARREL_VALUE
                        : FuelSceneConsts.POWERED_BARREL_VALUE;     
                    fuelScene.addCollectedFuel(value);
                }
                onExitDoor();
            }
            
            
            if (movementInput.sqrMagnitude > 0.5) {
                if (readyForInput) {
                    readyForInput = false;
                    player.Move(movementInput);

                    int boxesInPlace = boxes.ToList().FindAll(b => b.arrived).Count;
                    socobanLevelUI.setTotalBoxCollected(boxesInPlace);
                }
            } else {
                readyForInput = true;
            }
        }
    }

    public void onExitDoor() {       
        fuelScene.destroyLastCollidedDoor();
    }

    private bool IsLevelComplete() {        
        foreach (var box in boxes) {
            if (!box.arrived) {
                return false;
            }
        }
        return true;
    }

    private void onBoxArrived(SocobanBoxType boxType) {
        //fuelScene.addCollectedFuel(collectedValue);
    }

    private void onTipClosed(GameTipType tipType) {
        tipsManager.setTipWasShown(tipType, true);
    }
}
