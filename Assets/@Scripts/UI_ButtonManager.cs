using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonManager : MonoBehaviour
{
    public GameObject Map_A;
    public GameObject Map_B;
    // 초기 세팅: 맵 A에서 시작하도록 설정
    private bool _isMapActive;
    
    
    // 음영 이동 스킬 관련 오브젝트
    private bool _isCoolTime = false;
    [SerializeField] private float _coolTimeValue = 20f;
    [SerializeField] private Image _cooltimeLoopImage;
    [SerializeField] private TextMeshProUGUI _cooltimeTimer;
    [SerializeField] private GameObject _monsterSpawner;
    [SerializeField] private GameObject _UIShadowMove;

    [SerializeField] private UI_ShadowGauge UI_ShadowGauge;

    private void Start()
    {
        // 초기 세팅: 맵 A에서 시작하도록 설정
        _isMapActive = Map_A.activeSelf; // true
    }

    // 음영 이동 스킬
    public void OnClickShadowChange()
    {
        // 쿨타임이 진행 중이라면 버튼을 누를 수 없도록 처리
        if (_isCoolTime)
        {
            Console.WriteLine("음영 이동: 쿨타임");
            return;
        }
        
        if (!_isMapActive) // 맵 A인 경우
        {
            Map_A.SetActive(true);
            Map_B.SetActive(false);

            // 음영 게이지 비활성화
            //_UIShadowMove.SetActive(false);
            //StopCoroutine(ShadowGauge());

            _monsterSpawner.SetActive(true);            
            UI_ShadowGauge.StartSubtractingShadowGaugeOverTime();
        }
        else // 맵 B인 경우
        {
            Map_A.SetActive(false);
            Map_B.SetActive(true);

            // 음영 게이지 활성화
            //_UIShadowMove.SetActive(true);
            // StartCoroutine(ShadowGauge());

            _monsterSpawner.SetActive(false);
            UI_ShadowGauge.StartAddingShadowGaugeOverTime();
        }

        StartCoroutine(SkillCoolTime());
    }

    // 음영 이동 스킬 쿨타임
    private IEnumerator SkillCoolTime()
    {
        _isCoolTime = true; // *스킬 쿨타임 적용
        _isMapActive = !_isMapActive; // *맵 이동 적용
        float cooltimeMax = _coolTimeValue;
        float cooltime = cooltimeMax;

        if (_cooltimeTimer.gameObject.activeSelf == false)
            _cooltimeTimer.gameObject.SetActive(true);

        while (cooltime > 0f)
        {
            // 이미지의 오브젝트가 비활성화 상태인 경우, 활성화 상태로 전환한다.
            if (_cooltimeLoopImage.gameObject.activeSelf == false)
                _cooltimeLoopImage.gameObject.SetActive(true);

            // 쿨타임 카운팅 이미지 애니메이션 적용
            _cooltimeLoopImage.fillAmount = cooltime / cooltimeMax;

            // DeltaTime만큼 시간이 쿨타임이 감소하도록 설정
            cooltime -= Time.deltaTime;

            // 쿨타임 텍스트 출력
            string cooltimeText = TimeSpan.FromSeconds(cooltime).ToString("s\\:ff");
            string[] tokens = cooltimeText.Split(':');
            _cooltimeTimer.text = string.Format($"{tokens[0]}:{tokens[1]}");

            yield return new WaitForFixedUpdate();
        }

        _isCoolTime = false;
        _cooltimeTimer.gameObject.SetActive(false);
        MonsterSpawner.Instance.Active(true);
    }
}