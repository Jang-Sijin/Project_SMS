using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_EventHandler : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{   
    public Action OnClickHandler = null;
    public Action OnPressHandler = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickHandler?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPressHandler?.Invoke();
    }
}
