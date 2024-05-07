using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // 총알의 속도

    private Transform target; // 적 오브젝트의 위치를 저장하기 위한 변수

    // 적 오브젝트를 받아와서 설정하는 메서드
    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    void Update()
    {
        // 적 오브젝트가 설정되어 있지 않으면 총알을 발사하지 않음
        if (target == null)
        {
            Destroy(gameObject); // 총알 오브젝트를 파괴하여 메모리 누수 방지
            return;
        }

        // 적 방향으로 이동
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        // 총알이 적 오브젝트와 충돌하면 총알과 적을 파괴
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.1f) // 여기서 0.1f는 충돌 거리임
        {
            Destroy(gameObject); // 총알 오브젝트 파괴
            Destroy(target.gameObject); // 적 오브젝트 파괴
        }
    }
}