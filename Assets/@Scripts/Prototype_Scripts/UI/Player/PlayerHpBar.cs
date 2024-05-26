using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _hpBar = null;
    const float _minWidth = 1f;     

    void SetHpBar(int hp, int maxHp)
    {
        // HP 비율을 계산합니다.
        float ratio = hp / (float)maxHp;

        // HP 비율에 따라 스프라이트의 너비를 조정합니다.
        float newWidth = _minWidth * Mathf.Clamp01(ratio);
        _hpBar.size = new Vector2(newWidth, _hpBar.size.y);
    }

    public void SetHp(int hp, int maxHp)
    {
        SetHpBar(hp, maxHp);
        // SetHpText(hp);
    }
}
