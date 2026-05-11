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

    private GameArtType gameArtType;

    public void Awake()
    {
        mainSceneUI.setExitGameFunction(exitGame);
        mainSceneUI.setStartGameFunction(startGame);
        mainSceneUI.setResumeGameFunction(resumeGame);
        mainSceneUI.setOnToggleValueChange(onToggleChange);

        SpaceShipState? state = localDataManager.getSavedState();
        mainSceneUI.enableResumeButton(state != null);

        gameArtType = localDataManager.getArtType();
        mainSceneUI.setToggleValue(gameArtType == GameArtType.olgaArt);

        soundService.playMusic();
    }

    private void exitGame()
    {
        Application.Quit();
    }

    private void startGame()
    {
        sceneLoader.loadScene(OlgaSceneNumbers.STORY_SCENE_ID);
    }

    private void resumeGame()
    {
        sceneLoader.loadScene(OlgaSceneNumbers.GAME_PROGRESS_SCENE_ID);
    }

    private void onToggleChange(bool value)
    {
        GameArtType newArtType = value ? GameArtType.olgaArt : GameArtType.sofyaArt;
        localDataManager.saveGameArtType(newArtType);

        if (newArtType != gameArtType)
        {
            sceneLoader.loadScene(GameSceneType.main);
        }
    }
}
