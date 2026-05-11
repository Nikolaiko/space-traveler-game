using System;
using UnityEngine;
using UnityEngine.UI;

public class OlgaMainScreenUI : MonoBehaviour, MainSceneUI
{
    public Toggle graphicsToggle;
    public GameObject resumeButtonObject;
    private Action exitGameFunction;
    private Action startGameFunction;
    private Action resumeGameFunction;
    private Action<bool> changeToggleValueFunction;

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
