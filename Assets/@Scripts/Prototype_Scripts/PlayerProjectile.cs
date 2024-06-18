using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] int playerDamage = 5;

    [SerializeField] UI_ShadowGauge ShadowGauge_Images;
    private int gaugeValue;
    private int damageStack = 0;

    private void Start()
    {
        ShadowGauge_Images = GameObject.Find("ShadowGauge_Images").GetComponent<UI_ShadowGauge>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                // 대미지 공식 [시작]
                if (ShadowGauge_Images.FillValueShadowGauge > 0)
                    gaugeValue = (int)((ShadowGauge_Images.FillValueShadowGauge * 100) / (2 * 10));
                else
                    gaugeValue = 1;

                playerDamage = (int)(playerDamage * Mathf.Pow(2, gaugeValue));
                // 대미지 공식 [끝]

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
                // 대미지 공식 [시작]
                if (ShadowGauge_Images.FillValueShadowGauge > 0)
                    gaugeValue = (int)((ShadowGauge_Images.FillValueShadowGauge * 100) / (2 * 10));
                else
                    gaugeValue = 1;
                playerDamage = (int)(playerDamage * Mathf.Pow(2, gaugeValue));
                // 대미지 공식 [끝]

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