using UnityEngine;
using Zenject;

public class SuccessLaunchScene : MonoBehaviour
{
    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private LocalDataManager localDataManager;

    public StoryUIScreen storyScreen;

    public void Awake()
    {
        storyScreen.setCloseCallback(toMainMenu);
    }

    public void toMainMenu()
    {
        localDataManager.deleteSavedState();
        sceneLoader.loadScene(GameSceneType.main);
    }
}
