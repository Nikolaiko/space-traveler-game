using System;
using UnityEngine;

public class ArmorUI : MonoBehaviour, ArmorSceneUI
{
    public event Action onDoneButtonClick;

    public void doneButtonClick() {
        onDoneButtonClick?.Invoke();
    }
}
