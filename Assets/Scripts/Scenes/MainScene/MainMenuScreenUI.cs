using System;
using UnityEngine;

public class MainMenuScreenUI : MonoBehaviour, MainSceneUI
{
    public GameObject resumeButtonObject;
    private Action exitGameFunction;
    private Action startGameFunction;
    private Action resumeGameFunction;

    public void enableResumeButton(bool enabled)
    {
        resumeButtonObject.SetActive(enabled);
    }

    public void onExitGame()
    {
        exitGameFunction?.Invoke();
    }

    public void onStartGame()
    {
        startGameFunction?.Invoke();
    }

    public void onResume()
    {
        resumeGameFunction?.Invoke();
    }

    public void setExitGameFunction(Action action)
    {
        exitGameFunction = action;
    }

    public void setStartGameFunction(Action action)
    {
        startGameFunction = action;
    }

    public void setResumeGameFunction(Action action)
    {
        resumeGameFunction = action;
    }
}
