using System;
using UnityEngine;
using UnityEngine.UI;

public class BypassSchemePairsButton : MonoBehaviour
{
    private static float blinkDelay = 0.3f;
    
    public Sprite letterSprite;
    public Sprite crossSprite;
    public Sprite lightningSprite;
    public Sprite spiralSprite;
    public Sprite triangleSprite;
    public Sprite starSprite;
    public Action<BypassSchemePairsButton> clickAction;

    public Button buttonObject;
    public BypassButtonState buttonState;
    public Sprite closedSprite;    

    private Color color = Color.white;
    private bool isBlinking = false;
    private float lastTimeCount;
    private Image buttonImage;
    private Sprite openSprite;

    public void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void activate() {
        gameObject.SetActive(true);
    }

    public void hide() {
        buttonObject.interactable = false;
    }

    public void show() {        
        buttonObject.interactable = true;
    }

    public void setColor(Color buttonColor) {        
        color = buttonColor;
    }

    public void openButton() {
        buttonImage.sprite = mapStateToSprite(buttonState);
    }

    public void closeButton() {
        buttonImage.sprite = closedSprite;
    }

    public Color getColor() {
        return color;
    }

    public void blink() {
        isBlinking = true;
        lastTimeCount = Time.time;

        openButton();
    }

    public void onButtonClick() {
        if (isBlinking) return;
        
        clickAction?.Invoke(this);
    }

    public void Update() {
        if (!isBlinking) return;
        
        if (Time.time - lastTimeCount >= blinkDelay) {
            closeButton();
            isBlinking = false;
        }
    }

    private Sprite mapStateToSprite(BypassButtonState state) {
        switch (state)
        {
            case BypassButtonState.letter: {
                return letterSprite;                        
            }
            case BypassButtonState.cross: {                
                return crossSprite;
            }
            case BypassButtonState.lightning: {
                return lightningSprite;
            }
            case BypassButtonState.spiral: {
                return spiralSprite;
            }
            case BypassButtonState.star: {
                return starSprite;
            }
            case BypassButtonState.triangle: {
                return triangleSprite;
            }
            default: {
                return openSprite;        
            }
        }
    }
}
