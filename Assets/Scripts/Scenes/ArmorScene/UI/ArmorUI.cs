using System;
using TMPro;
using UnityEngine;

public class ArmorUI : MonoBehaviour, ArmorSceneUI
{
    public event Action onDoneButtonClick;

    public TMP_Text timeLeftText;
    public GameObject timeIsUpText;

    public void doneButtonClick() {
        onDoneButtonClick?.Invoke();
    }

    public void updateTimeLeft(float timeLeft) {
        int seconds = Mathf.CeilToInt(timeLeft);
        timeLeftText.text = $"Время: {seconds / 60}:{seconds % 60:00}";
    }

    public void showTimeIsUp() {
        timeIsUpText.SetActive(true);
    }
}
