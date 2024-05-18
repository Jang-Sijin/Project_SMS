using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 2f; // 몬스터 이동 속도
    public int hp = 100; // 몬스터 체력
    public GameObject itemPrefab; // 드롭할 아이템 프리팹
    private Transform player;
    [HideInInspector]
    public MonsterSpawner spawner;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 플레이어 방향으로 이동
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
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
                playerScript.hp -= 1;
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
}
