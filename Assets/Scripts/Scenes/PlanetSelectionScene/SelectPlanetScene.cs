using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SelectPlanetScene : MonoBehaviour
{
    [Inject]
    private SelectPlanetSceneUI planetSceneUI;

    [Inject]
    private PlanetsInfoLoader planetsInfoLoader;

    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private LocalDataManager dataManager;

    [Inject]
    private ShipBaseParametersCalculator calculator;

    private List<DestinationPlanetInfo> planetInfos = new List<DestinationPlanetInfo>();
    private DestinationPlanetInfo? selectedPlanetInfo;

    public void Start() {
        planetSceneUI.onPlanetSelect += onPlanetSelect;
        planetSceneUI.onStartButtonClick += onStartButtonClick;

        planetInfos = planetsInfoLoader.loadPlanetsInfo();
    }

    private void onPlanetSelect(DestinationPlanetType planetType) {
        try {
            DestinationPlanetInfo info = planetInfos.Where(value => value.planetType == planetType).First();
            planetSceneUI.setSelectedPlanetStats(info.distance, info.obstacles);
            selectedPlanetInfo = info;
        } catch (ArgumentNullException argumentException) {
            Debug.LogException(argumentException);
        } catch (InvalidOperationException operationException) {
            Debug.LogException(operationException);
        }
    }

    private void onStartButtonClick() {
        if (selectedPlanetInfo.HasValue) {            
            int distance = selectedPlanetInfo.Value.distance;
            int obstacles = selectedPlanetInfo.Value.obstacles;
            dataManager.saveGameState(buildInitialSpaceShipState(
                selectedPlanetInfo.Value.planetType,
                distance,
                obstacles)
            );
            sceneLoader.loadScene(OlgaSceneNumbers.GAME_PROGRESS_SCENE_ID);
        }        
    }

    private SpaceShipState buildInitialSpaceShipState(DestinationPlanetType planetType, int distance, int obstacles) {
        int armorNeeded = calculator.calculateBaseArmorNeededForDistance(distance, obstacles);
        return new SpaceShipState(
            planetType: selectedPlanetInfo.Value.planetType,
            armorNeeded: armorNeeded,
            foodNeeded: calculator.calculateBaseFoodNeededForDistance(distance),
            armorCollected: armorNeeded,
            armorWeight: armorNeeded
        );
    }
}
