using System;

public interface ArmorSceneUI
{
    event Action onDoneButtonClick;

    void updateTimeLeft(float timeLeft);
    void showTimeIsUp();
}
