using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main_Proto : MonoBehaviour
{
    public Button ShadowMoveButton;    
    public UI_ShadowGauge ShadowGauge;

    //private UI_ButtonManager ShadowMoveButtonManager;

    void Start()
    {
        // 쉐도우 게이지 버튼
        ShadowMoveButton.gameObject.SetActive(true);

        //// UI_ButtonManager 가져오기
        //ShadowMoveButtonManager = ShadowMoveButton.GetComponent<UI_ButtonManager>();
    }
}
