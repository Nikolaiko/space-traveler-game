using UnityEngine;
using Zenject;

public class MainScene : MonoBehaviour
{
    [Inject]
    private MainSceneUI mainSceneUI;

    [Inject]
    private LocalDataManager localDataManager;

    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private SoundService soundService;

    public void Awake()
    {
        mainSceneUI.setExitGameFunction(exitGame);
        mainSceneUI.setStartGameFunction(startGame);
        mainSceneUI.setResumeGameFunction(resumeGame);

        SpaceShipState? state = localDataManager.getSavedState();
        mainSceneUI.enableResumeButton(state != null);

        UserSettings settings = localDataManager.getUserSettings();
        if (settings.musicOn) {
            soundService.playMusic();    
        }        
    }

    private void exitGame()
    {
        Application.Quit();
    }

    private void startGame()
    {
        sceneLoader.loadScene(GameSceneType.story);
    }

    private void resumeGame()
    {
        sceneLoader.loadScene(GameSceneType.gameProgress);
    }
}
