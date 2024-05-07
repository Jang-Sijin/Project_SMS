using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 총알이 발사될 위치
    public float fireRate = 1f; // 발사 간격 (초 단위)

    private float nextFireTime; // 다음 발사 시간

    void Update()
    {
        // 적의 동작 코드...

        // 총알 발사 간격을 체크하여 발사
        if (Time.time >= nextFireTime)
        {
            //FireBullet();
            nextFireTime = Time.time + 1f / fireRate; // 다음 발사 시간 업데이트
        }
    }

    //void FireBullet()
    //{
    //    if (bulletPrefab != null && firePoint != null)
    //    {
    //        // 모든 적 오브젝트를 가져와서 총알을 발사
    //        foreach (GameObject enemy in FindAllEnemies())
    //        {
    //            // 총알 오브젝트 생성
    //            GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

    //            // 총알 오브젝트의 방향 설정
    //            Bullet bullet = bulletGo.GetComponent<Bullet>();
    //            if (bullet != null)
    //            {
    //                bullet.SetTarget(enemy.transform);
    //            }
    //        }
    //    }
    //}

    //GameObject[] FindAllEnemies()
    //{
    //    // 모든 적 오브젝트를 배열로 반환하는 로직
    //}
}
