using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShadowGauge : MonoBehaviour
{
    [SerializeField] private Image _shadowGaugeImage;
    public float FillValueShadowGauge => _fillValueShadowGauge;
    private float _fillValueShadowGauge = 0f;
    private const float MinShadowGauge = 0;
    private const float MaxShadowGauge = 1;

    [Header("암흑 게이지 증가율 값")]
    public float ShadowGaugeIncreaseRate = 0.02f;
    [Header("암흑 게이지 증가 속도(Seconds)")]
    public float IncreaseRateSeconds = 1.0f;

    [Header("암흑 게이지 감소율 값")]
    public float ShadowGaugeDecreaseRate = 0.01f;
    [Header("암흑 게이지 감소 속도(Seconds)")]
    public float DecreaseRateSeconds = 0.2f;

    private Coroutine _changeGaugeCoroutine = null;

    public void StartAddingShadowGaugeOverTime()
    {
        if (_changeGaugeCoroutine != null)
            StopCoroutine(_changeGaugeCoroutine);

        _changeGaugeCoroutine = StartCoroutine(ChangeShadowGaugeOverTime(ShadowGaugeIncreaseRate));
    }

    public void StartSubtractingShadowGaugeOverTime()
    {
        if (_changeGaugeCoroutine != null)
            StopCoroutine(_changeGaugeCoroutine);

        _changeGaugeCoroutine = StartCoroutine(ChangeShadowGaugeOverTime(-ShadowGaugeDecreaseRate));
    }

    private IEnumerator ChangeShadowGaugeOverTime(float changeRate)
    {                     
        while (true)
        {
            if(changeRate > 0)
                yield return new WaitForSeconds(IncreaseRateSeconds); // Wait for 1 second
            else
                yield return new WaitForSeconds(DecreaseRateSeconds); // Wait for 0.2 second

            _fillValueShadowGauge = Mathf.Min(_fillValueShadowGauge + changeRate, MaxShadowGauge); // Increase gauge value
            UpdateShadowGaugeUI();
        }             
    }

    private void UpdateShadowGaugeUI()
    {
        _shadowGaugeImage.fillAmount = _fillValueShadowGauge;
    }

    // 음영 게이지 - 초기화
    public void ResetGauge()
    {
        _shadowGaugeImage.fillAmount = 0f;
        _fillValueShadowGauge = 0f;
    }
}
