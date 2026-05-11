using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomStateButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    public GameObject selectedState;
    public GameObject normalState;
    public Action onClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedState.SetActive(true);
        normalState.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectedState.SetActive(false);
        normalState.SetActive(true);
    }
}
