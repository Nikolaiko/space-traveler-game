using System;
using UnityEngine;
using UnityEngine.UI;

public class SingleButtonScreen : MonoBehaviour
{
    public Sprite screenSprite;
    public Action onClick;

    private Image image;

    public void Start()
    {
        image = GetComponentInChildren<Image>();
        image.sprite = screenSprite;
    }

    public void buttonClicked()
    {
        onClick.Invoke();
    }

    public void showScreen()
    {
        gameObject.SetActive(true);
    }
}
