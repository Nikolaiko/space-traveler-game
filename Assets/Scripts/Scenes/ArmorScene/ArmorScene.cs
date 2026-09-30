using System.Linq;
using UnityEngine;
using Zenject;

public class ArmorScene : MonoBehaviour
{
    [Inject]
    private ArmorSceneUI sceneUI;

    [Inject]
    private LocalDataManager localDataManager;

    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private PlanetsInfoLoader planetsInfoLoader;

    public ScrapConveyor conveyor;

    private ArmorRound round;

    public void Awake() {
        sceneUI.onDoneButtonClick += finishCollecting;
    }

    public void Start() {
        SpaceShipState? shipState = localDataManager.getSavedState();
        if (!shipState.HasValue) {
            Debug.LogWarning("ArmorScene: no saved ship state, returning to game progress");
            sceneLoader.loadScene(GameSceneType.gameProgress);
            return;
        }

        round = new ArmorRound(
            ArmorCollectionConsts.roundDuration,
            ArmorCollectionConsts.beltStartSpeed,
            ArmorCollectionConsts.beltEndSpeed
        );
        round.onFinished += finishRound;

        int obstacles = planetObstacles(shipState.Value.planetType);
        conveyor.startBelt(new ScrapBag(
            ArmorCollectionConsts.scrapBagContents(obstacles),
            ArmorCollectionConsts.maxSameTypeInRow,
            new System.Random()
        ));
        conveyor.speed = round.beltSpeed;
        sceneUI.updateTimeLeft(round.timeLeft);
    }

    public void Update() {
        if (round == null || round.finished) {
            return;
        }

        round.tick(Time.deltaTime);
        conveyor.speed = round.beltSpeed;
        sceneUI.updateTimeLeft(round.timeLeft);
    }

    // Время вышло: лента встаёт, добыча раунда переходит на экран итога (#42)
    private void finishRound(ArmorLoot loot) {
        conveyor.stopBelt();
        sceneUI.showTimeIsUp();
    }

    private void finishCollecting() {
        SpaceShipState? shipState = localDataManager.getSavedState();
        if (shipState.HasValue) {
            ArmorLoot loot = temporaryLoot(shipState.Value.armorNeeded ?? 0);
            localDataManager.saveGameState(loot.applyTo(shipState.Value));
        }

        sceneLoader.loadScene(GameSceneType.gameProgress);
    }

    // Временный фиксированный результат, пока нет самой игры:
    // стальные листы, пока не наберётся нужная броня
    private ArmorLoot temporaryLoot(int armorNeeded) {
        ArmorLoot loot = new ArmorLoot();
        while (loot.armor < armorNeeded) {
            loot.add(ScrapType.steel);
        }
        return loot;
    }

    private int planetObstacles(DestinationPlanetType planetType) {
        return planetsInfoLoader.loadPlanetsInfo()
            .Where(value => value.planetType == planetType)
            .First()
            .obstacles;
    }
}
