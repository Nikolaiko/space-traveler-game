using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OlgaPlanetObjectUI : MonoBehaviour, IPointerClickHandler
{
    private static Vector3 normalScale = new Vector3(1.0f, 1.0f, 1.0f);
    private static Vector3 selectedScale = new Vector3(1.3f, 1.3f, 1.0f);
    private static float animationDuration = 0.5f;
    
    public Image glowImage;
    public Animator planetAnimator;
    public Action onPlanetClick;

    private Vector3 targetScale = normalScale;
    private RectTransform rectTransform;
    private float scaleTimer = 0.0f;
    private Coroutine scaleCoroutine;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onPlanetClick();
    }

    public void setSelected(bool selected)
    {
        planetAnimator.enabled = selected;
        glowImage.enabled = selected;

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        
        if (selected)
        {
            
            scaleUp();
        }
        else
        {
            scaleDown();
        }
    }

    private void scaleUp()
    {
        scaleCoroutine = StartCoroutine(scaleProcess(normalScale, selectedScale));
    }

    private void scaleDown()
    {
        scaleCoroutine = StartCoroutine(scaleProcess(selectedScale, normalScale));
    }
    
    private IEnumerator scaleProcess(Vector3 startScale, Vector3 endScale)
    {
        scaleTimer = 0.0f;
        rectTransform.localScale = startScale;

        while (scaleTimer < animationDuration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, scaleTimer / animationDuration);
            scaleTimer += Time.deltaTime;
            yield return null;
        }
        rectTransform.localScale = endScale;
    }
}
