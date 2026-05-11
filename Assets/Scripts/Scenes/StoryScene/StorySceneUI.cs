using UnityEngine;
using Zenject;

public class StorySceneUI : MonoBehaviour
{
    public StoryUIScreen comixScreen;

    [Inject]
    private SceneLoader sceneLoader; 

    public void Start()
    {
        comixScreen.setCloseCallback(onSkipStory);
    }

    public void onSkipStory() {        
        sceneLoader.loadScene(OlgaSceneNumbers.SELECT_PLANET_SCENE_ID);
    }
}
