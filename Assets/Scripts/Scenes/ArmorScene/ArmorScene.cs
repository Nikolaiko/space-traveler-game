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
            // Временный фиксированный результат, пока нет самой игры:
            // ровно нужная броня стальными листами, у них вес равен броне.
            int armor = shipState.Value.armorNeeded ?? 0;
            localDataManager.saveGameState(shipState.Value.copy(
                armorCollected: armor,
                armorWeight: armor
            ));
        }

        sceneLoader.loadScene(GameSceneType.gameProgress);
    }
}
