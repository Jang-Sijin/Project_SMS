﻿using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] int playerDamage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(playerDamage); // 예시로 10의 피해를 줌
                StartCoroutine(HandleHit(monster));
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("BossMonster"))
        {
            BoosMonster bossMonster = collision.GetComponent<BoosMonster>();
            if (bossMonster != null)
            {
                bossMonster.TakeDamage(playerDamage); // 예시로 10의 피해를 줌
                StartCoroutine(HandleHit(bossMonster));
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator HandleHit(Monster monster)
    {
        SpriteRenderer renderer = monster.GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;
        renderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        if (monster.Hp > 0)
        {
            renderer.color = originalColor;
        }
    }

    private IEnumerator HandleHit(BoosMonster monster)
    {
        SpriteRenderer renderer = monster.GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;
        renderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        if (monster.Hp > 0)
        {
            renderer.color = originalColor;
        }
    }
}