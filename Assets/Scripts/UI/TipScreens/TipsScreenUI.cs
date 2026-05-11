using System;
using UnityEngine;

public class TipsScreenUI : MonoBehaviour
{
    public GameTipType type;

    public Action onCloseScreen;

    public void Start() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.localScale = new Vector2(1, 1);
    }
    
    public void setParent(Transform parentTransform) {
        transform.SetParent(parentTransform, false);
    }

    public void removeFromParent() {
        transform.SetParent(null);  
        Destroy(gameObject);
    }

    public void onCloseButtonClick() {
        onCloseScreen?.Invoke();
    }
}
