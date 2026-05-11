using UnityEngine;
using UnityEngine.UI;

public class BypassScemePairRow : MonoBehaviour {
    public Sprite openSprite;
    public Sprite closeSprite;

    private Image rowImage;

    private BypassPairStatus pairStatus = BypassPairStatus.locked;

    public void Awake()
    {
        rowImage = GetComponent<Image>();
    }

    public void setStatus(BypassPairStatus status) {
        pairStatus = status;
        switch (status)
        {
            case BypassPairStatus.locked: {
                rowImage.sprite = closeSprite;
                break;        
            }
            case BypassPairStatus.unlocked: {
                rowImage.sprite = openSprite;
                break;        
            }
        }
    }

    public BypassPairStatus getStatus() {
        return pairStatus;
    }
}
