using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Player는 State 패턴을 통해 상태가 전이되도록 구현 예정
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public GameObject projectilePrefab; // 발사할 스프라이트 프리팹
    public Transform firePoint; // 발사 위치
    public float projectileSpeed = 10f; // 발사체 속도    
    public float attackInterval = 0.1f; // 공격 주기 (초)

    [SerializeField] PlayerHpBar _playerHpBar;

    private int _hpMax = 100;
    public int HpMax { get { return _hpMax; } }
    private int _hp = 100; // 플레이어 체력
    public int Hp 
    { 
        get { return _hp; } 
        set 
        {
            _hp = value;
            if (_playerHpBar != null && HpMax > 0)
                _playerHpBar.SetHp(_hp, HpMax);
        } 
    }

    private Rigidbody2D rb;
    private Vector2 movement;

    // ------------------------------------------------------    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        StartCoroutine(AutoAttack());
    }

    private void Update()
    {
        // 키 입력 받기
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // 이동 방향 설정
        movement = new Vector2(moveHorizontal, moveVertical).normalized;
    }

    private void FixedUpdate()
    {
        // 플레이어 이동
        MovePlayer(movement);
    }

    private void MovePlayer(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void Fire()
    {
        GameObject nearestMonster = FindNearestMonster();
        if (nearestMonster != null)
        {
            Vector2 direction = (nearestMonster.transform.position - firePoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
            rbProjectile.velocity = direction * projectileSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private GameObject FindNearestMonster()
    {
        // 보스는 우선 1마리로 출현된다고 가정한다.
        GameObject boss = GameObject.FindGameObjectWithTag("BossMonster");

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");        

        GameObject nearestMonster = null;
        float minDistance = Mathf.Infinity;

        if(boss != null)
        {
            float distance = Vector2.Distance(transform.position, boss.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestMonster = boss;
            }
        }

        foreach (GameObject monster in monsters)
        {
            float distance = Vector2.Distance(transform.position, monster.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestMonster = monster;
            }
        }

        return nearestMonster;
    }

    // 대미지를 입었을 경우
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;            
            gameObject.SetActive(false);
        }
    }
}
