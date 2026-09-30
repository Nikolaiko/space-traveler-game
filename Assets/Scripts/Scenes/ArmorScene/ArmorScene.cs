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

    public void Awake() {
        sceneUI.onDoneButtonClick += finishCollecting;
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
}
