using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameProgressScene : MonoBehaviour, CoroutineScope
{
    [Inject]
    private LocalDataManager localDataManager;

    [Inject]
    private PlanetsInfoLoader planetsInfoLoader;

    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private ShipBaseParametersCalculator parametersCalculator;

    [Inject]
    private TipsManager tipsManager;

    [Inject]
    private GameProgressUI gameProgressUI;

    public void Awake() {
        gameProgressUI.onTipScreenClosed = onTipScreenClose;
        gameProgressUI.onFuelButtonTap = startCollectingFuel;
        gameProgressUI.onShipParameterClose = checkScreenState;
        gameProgressUI.onShipParameterUpdateValue = checkScreenState;
        gameProgressUI.onFoodButtonTap = startCollectingFood;
        gameProgressUI.onLaunchButtonTap = tryToLaunch;
        
        bool calculatorWasShown = tipsManager.tipWasShown(GameTipType.parameterCalculatorTip);        
        if (!calculatorWasShown) {
            gameProgressUI.showTip(GameTipType.parameterCalculatorTip);
        }

        checkScreenState();
    }

    public void Start() {
        SpaceShipState? shipState = localDataManager.getSavedState();
        if (shipState.HasValue) {
            setComponentValuesFromState(shipState.Value);
        } else {
            setDefaultComponentValues();
        }
    }

    #region Buttons Callbacks

    public void startCollectingArmor() {
        gameProgressUI.showArmorDevelopmentBanner();
    }

    public void startCollectingFood() {
        goToScene(OlgaSceneNumbers.SCENE_2048_ID);
    }

    public void startCollectingFuel() {
        sceneLoader.loadScene(OlgaSceneNumbers.FUEL_SCENE_ID);
    }

    public void tryToLaunch() {
        SpaceShipState? shipState = localDataManager.getSavedState();
        if (shipState.HasValue) {
            try {
                int fuelNeeded = shipState.Value.fuelNeeded ?? int.MaxValue;
                if (fuelNeeded <= shipState.Value.fuelCollected) {                    
                    sceneLoader.loadScene(OlgaSceneNumbers.SUCCESS_SCENE_ID);
                } else {
                    sceneLoader.loadScene(OlgaSceneNumbers.FAIL_SCENE_ID);
                }
            } catch (ArgumentNullException argumentException) {
                Debug.LogException(argumentException);
            } catch (InvalidOperationException operationException) {
                Debug.LogException(operationException);
            }
        } else {
            setDefaultComponentValues();
        }        
    }

    public void onShipParameterClick() {
        SpaceShipState? shipState = localDataManager.getSavedState();
        if (shipState.HasValue) {
            gameProgressUI.showShipParametersScreen(shipState.Value);
        }        
    }

    #endregion

    private void onTipScreenClose(GameTipType tipType) {
        tipsManager.setTipWasShown(tipType, true);
        
        switch (tipType) {
            case GameTipType.foodCollectionTip: {
                goToScene(OlgaSceneNumbers.SCENE_2048_ID);
                break;
            }
            case GameTipType.fuelCollectionTip: {
                goToScene(OlgaSceneNumbers.FUEL_SCENE_ID);
                break;
            }
            case GameTipType.armorCollectionTip: {                
                break;
            }
        }
    }

    #region CoroutineScope Interface
    public void launch(IEnumerator routine) {
        StartCoroutine(routine);
    }

    #endregion

    private void goToScene(int sceneId) {
        sceneLoader.loadScene(sceneId);
    }

    private void setComponentValuesFromState(SpaceShipState state) {        
        gameProgressUI.setFoodState(state.foodCollectionState());
        gameProgressUI.setFoodWeight(state.foodWeight);        
    }

    private void setDefaultComponentValues() {
        gameProgressUI.setFoodState(CollectionState.inProgress);
        gameProgressUI.setFoodWeight(0);

        gameProgressUI.setFuelState(CollectionState.inProgress);        
    }

    private void checkScreenState() {
        SpaceShipState? state = localDataManager.getSavedState();
        if (state.HasValue) {
            bool needCollectFood = state.Value.foodCollectionState() == CollectionState.inProgress;
            bool needCollectFuel = state.Value.fuelNeeded.HasValue && state.Value.fuelCollectionState() == CollectionState.inProgress;

            gameProgressUI.setFuelQuestButtonEnabled(needCollectFuel);
            gameProgressUI.setFoodQuestButtonEnabled(needCollectFood);

            gameProgressUI.setFuelState(state.Value.fuelCollectionState());

            if (state.Value.readyToFly()) {                
                gameProgressUI.enableLaunchButton();
            } else {
                gameProgressUI.disbaleLaunchButton();
            }

            if (needCollectFood)
            {
                gameProgressUI.disbaleShipParameterButton();
            } else
            {
                gameProgressUI.enableShipParameterButton();
            }
        }
    }
}
