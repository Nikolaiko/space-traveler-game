using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class StartSceneAssigner
{
    static StartSceneAssigner()
    {
        // Path to your desired editor startup scene
        string scenePath = "Assets/Scenes/MainMenuScene.unity"; 
        
        SceneAsset myScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        
        if (myScene != null)
        {
            EditorSceneManager.playModeStartScene = myScene;
        }
    }
}

