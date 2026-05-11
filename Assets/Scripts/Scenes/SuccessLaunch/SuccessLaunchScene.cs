using UnityEngine;

public class SuccessLaunchScene : MonoBehaviour
{
    private UnitySceneLoader sceneLoader = new UnitySceneLoader(null);

    public void toMainMenu() {
        sceneLoader.loadScene(OlgaSceneNumbers.MAIN_SCENE_ID);
    }
}
