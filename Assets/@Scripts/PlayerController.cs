using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public GameObject projectilePrefab; // 발사할 스프라이트 프리팹
    public Transform firePoint; // 발사 위치
    public float projectileSpeed = 10f; // 발사체 속도
    public int hp = 100; // 플레이어 체력
    public float attackInterval = 0.1f; // 공격 주기 (초)

    private Rigidbody2D rb;
    private Vector2 movement;

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
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject nearestMonster = null;
        float minDistance = Mathf.Infinity;

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
}
