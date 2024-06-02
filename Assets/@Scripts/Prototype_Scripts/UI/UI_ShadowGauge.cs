using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShadowGauge : MonoBehaviour
{
    [SerializeField] private Image _shadowGaugeImage;

    public void UpdateShadowGaugeUI()
    {
        //_shadowGaugeImage.fillAmount = GameMaanger.Instance.ShadowGaugeValue / GameMaanger.Instance.ShadowGaugeMax;
    }

    // 음영 게이지 - 초기화
    public void ResetGauge()
    {
        _shadowGaugeImage.fillAmount = 0f;
    }
}
