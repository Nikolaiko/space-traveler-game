using System;
using UnityEngine;

public class BasicUIScreen : MonoBehaviour
{
    public Action onCloseScreen;

    public void onCloseButtonClick() {
        onCloseScreen?.Invoke();
    }

    public void show() {
        gameObject.SetActive(true);
    }

    public void hide() {
        gameObject.SetActive(false);
    }
}
