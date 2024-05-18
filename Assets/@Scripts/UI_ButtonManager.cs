using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonManager : MonoBehaviour
{
    public GameObject Map_A;
    public GameObject Map_B;

    public void OnClickChangeScene()
    {
        // 초기 세팅: 맵 A에서 시작하도록 설정
        bool isActive = Map_A.activeSelf;

        // 
        if (!isActive)
        {
            Map_A.SetActive(true);
            Map_B.SetActive(false);            
        }
        else
        {
            Map_A.SetActive(false);
            Map_B.SetActive(true);
        }
    }
}
