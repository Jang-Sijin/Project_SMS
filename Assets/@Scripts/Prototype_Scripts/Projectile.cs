using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(10); // 예시로 10의 피해를 줌
                StartCoroutine(HandleHit(monster));
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
        if (monster.hp > 0)
        {
            renderer.color = originalColor;
        }
    }
}