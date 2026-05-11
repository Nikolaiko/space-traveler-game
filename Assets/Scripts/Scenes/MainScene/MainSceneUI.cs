using System;

interface MainSceneUI
{
    void setExitGameFunction(Action action);
    void setStartGameFunction(Action action);
    void setResumeGameFunction(Action action);
    void enableResumeButton(bool enable);
    void setToggleValue(bool enable);
    void setOnToggleValueChange(Action<bool> action);
}