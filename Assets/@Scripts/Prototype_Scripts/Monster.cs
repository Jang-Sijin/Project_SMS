using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 2f; // 몬스터 이동 속도    
    private int _damage = 5; // 몬스터 공격력
    public GameObject itemPrefab; // 드롭할 아이템 프리팹
    private Transform player;
    [HideInInspector] public MonsterSpawner spawner;
    private SpriteRenderer spriteRenderer;

    public int HpMax = 100;
    private int _hp = 100; // 몬스터 체력
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_monsterHpBar != null && HpMax > 0)
                _monsterHpBar.SetHp(_hp, HpMax);
        }
    }

    [SerializeField] PlayerHpBar _monsterHpBar;
    public int Damage { get { return _damage; } private set { } }


    #region 발사체
    public GameObject projectilePrefab; // 발사할 스프라이트 프리팹
    public Transform firePoint; // 발사 위치
    public float projectileSpeed = 3f; // 발사체 속도    
    public float attackInterval = 1f; // 공격 주기 (초)
    #endregion

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 발사체 코루틴 시작
        StartCoroutine(AutoAttack());
    }

    void Update()
    {
        // 플레이어 방향으로 이동
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            if (spawner != null)
            {
                spawner.RemoveMonster(gameObject);
            }
            DropItem();
            gameObject.SetActive(false);
        }
    }

    private void DropItem()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerScript = collision.GetComponent<PlayerController>();
            if (playerScript != null)
            {
                playerScript.Hp -= _damage;
            }
            if (spawner != null)
            {
                spawner.RemoveMonster(gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.RemoveMonster(gameObject);
        }
    }


    #region 발사체 로직
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
        GameObject nearestMonster = FindNearestPlayer();
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

    private GameObject FindNearestPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject nearestMonster = null;
        float minDistance = Mathf.Infinity;


        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < minDistance)
        {
            minDistance = distance;
            nearestMonster = player;
        }

        return nearestMonster;
    }

    #endregion
}
