using UnityEngine;
using Zenject;

public class GameOptionsScreen : MonoBehaviour
{
    [Inject]
    private SceneLoader sceneLoader;

    public void onQuitGameClick()
    {
        Application.Quit();
    }

    public void onGotoMainMenuClick()
    {
        sceneLoader.loadScene(OlgaSceneNumbers.MAIN_SCENE_ID);
    }

    public void onCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
