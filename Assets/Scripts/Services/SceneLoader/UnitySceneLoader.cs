using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitySceneLoader : SceneLoader
{
    private AsyncOperation loadOperation = null;

    private LocalDataManager dataManager;

    public UnitySceneLoader(LocalDataManager dataManager)
    {
        this.dataManager = dataManager;
    }

    public void loadScene(GameSceneType sceneType)
    {
        GameArtType gameArtType = dataManager.getArtType();
        loadScene(resolveArtTypeAndSceneType(gameArtType, sceneType));
    }

    public void loadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void loadSceneAsyncAdditive(int sceneNumber, Action completion, CoroutineScope scope)
    {
        if (loadOperation != null) { return; }
        scope.launch(loadAsyncScene(sceneNumber, completion));
    }

    public void unloadScene(int sceneNumber, Action completion, CoroutineScope scope)
    {
        scope.launch(unloadAsyncScene(sceneNumber, completion));
    }

    private IEnumerator unloadAsyncScene(int sceneNumber, Action completion)
    {
        try
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneNumber);
            while (!unloadOperation.isDone)
            {
                yield return null;
            }
        }
        finally
        {
            completion();
        }
    }

    private IEnumerator loadAsyncScene(int sceneNumber, Action completion)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        loadOperation = null;
        completion();
    }

    private int resolveArtTypeAndSceneType(GameArtType artType, GameSceneType sceneType)
    {
        switch (artType)
        {
            case GameArtType.sofyaArt:
                return SofiaSceneNumbers.sceneNumberFromSceneType(sceneType);
            case GameArtType.olgaArt:
                return OlgaSceneNumbers.sceneNumberFromSceneType(sceneType);
            default:
                return OlgaSceneNumbers.sceneNumberFromSceneType(sceneType);
        }
    }
}
