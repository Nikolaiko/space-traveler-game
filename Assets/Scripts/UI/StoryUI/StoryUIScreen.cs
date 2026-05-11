using System;
using UnityEngine;
using UnityEngine.UI;

public class StoryUIScreen : MonoBehaviour
{
    public Sprite[] comixFrames;
    public Button skipButton;
    public Button nextFrameButton;
    public Button continueButton;


    protected Image comixImage;
    protected int currentFrame = 0;
    private Action onClose;    
    private int maxFrames = 0;

    public virtual void Start()
    {
        comixImage = GetComponent<Image>();
        maxFrames = comixFrames.Length - 1;

        continueButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(true);
        nextFrameButton.gameObject.SetActive(true);

        updateComixFrame();
    }

    public void nextFrame()
    {
        if (currentFrame < maxFrames)
        {
            currentFrame += 1;
            updateComixFrame();

            if (currentFrame == maxFrames)
            {
                showCloseButton();
            }
        }
    }

    public void onSkipStory() {        
        currentFrame = maxFrames;
        updateComixFrame();
        showCloseButton();
    }

    public void onContinue()
    {
        onClose.Invoke();    
    }

    public void setCloseCallback(Action closeAction)
    {
        onClose = closeAction;
    }

    protected virtual void updateComixFrame()
    {
        comixImage.sprite = comixFrames[currentFrame];
    }

    private void showCloseButton()
    {
        continueButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(false);
        nextFrameButton.gameObject.SetActive(false);
    }
}
