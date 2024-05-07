using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonManager : MonoBehaviour
{
    public GameObject Map_A;
    public GameObject Map_B;

    public void OnClickChangeSceneA()
    {
        bool isActive = Map_A.activeSelf;

        if (!isActive)
        {
            Map_A.SetActive(true);
            Map_B.SetActive(false);
        }
    }

    public void OnClickChangeSceneB()
    {
        bool isActive = Map_B.activeSelf;

        if (!isActive)
        {
            Map_B.SetActive(true);
            Map_A.SetActive(false);
        }
    }
}
