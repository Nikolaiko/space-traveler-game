using System;

public interface SceneLoader {
    void loadScene(GameSceneType sceneType);
    void loadScene(int sceneNumber);
    void loadSceneAsyncAdditive(int sceneNumber, Action completion, CoroutineScope scope);
    void unloadScene(int sceneNumber, Action completion, CoroutineScope scope);
}