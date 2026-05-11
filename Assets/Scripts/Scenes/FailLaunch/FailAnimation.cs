using UnityEngine;
using Zenject;

public class FailAnimation : MonoBehaviour
{
    [Inject]
    private SceneLoader sceneLoader;


    [Inject]
    private LocalDataManager localDataManager;
    
    public StoryWithTextUIScreen storyScreen;

    public void Awake()
    {
        storyScreen.setCloseCallback(endScene);
    }

    private void endScene()
    {
        localDataManager.deleteSavedState();
        sceneLoader.loadScene(OlgaSceneNumbers.MAIN_SCENE_ID);
    }
}
