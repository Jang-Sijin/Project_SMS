using System.Collections;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] int MonsterDamage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(MonsterDamage); // 예시로 10의 피해를 줌
                
                // 플레이어 히트 => 효과 현재 미적용
                // tartCoroutine(HandleHit(player));
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator HandleHit(PlayerController player)
    {
        SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;
        renderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        if (player.Hp > 0)
        {
            renderer.color = originalColor;
        }
    }
}