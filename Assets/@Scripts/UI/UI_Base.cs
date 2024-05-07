using System;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    public void BintEvent(GameObject go, Action action, Define.EventType eventType = Define.EventType.OnClick)
    {
        UI_EventHandler eventHandler = go.GetOrAddComponent<UI_EventHandler>();

        switch (eventType)
        {
            case Define.EventType.OnClick:
                eventHandler.OnClickHandler -= action;
                eventHandler.OnClickHandler += action;
                break;
            case Define.EventType.OnPress:
                eventHandler.OnPressHandler -= action;
                eventHandler.OnPressHandler += action;
                break;
        }
    }
}