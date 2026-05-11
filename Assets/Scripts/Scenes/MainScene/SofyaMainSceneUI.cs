using System;
using UnityEngine;
using UnityEngine.UI;

public class SofyaMainSceneUI : MonoBehaviour, MainSceneUI
{
    public Toggle graphicsToggle;
    public GameObject resumeButtonObject;
    public CustomStateButton newGameButton;
    public CustomStateButton resumeGameButton;
    public CustomStateButton exitGameButton;
    private Action exitGameFunction;
    private Action startGameFunction;
    private Action resumeGameFunction;
    private Action<bool> changeToggleValueFunction;

    public void Start()
    {
        newGameButton.onClick = startGameFunction;
        resumeGameButton.onClick = resumeGameFunction;
        exitGameButton.onClick = exitGameFunction;
    }

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

    public void setToggleValue(bool value)
    {
        graphicsToggle.isOn = value;
    }

    public void onToggleChange(bool value)
    {
        print("Change!!!");
        changeToggleValueFunction(value);
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

    public void setOnToggleValueChange(Action<bool> action)
    {
        changeToggleValueFunction = action;
    }
}
