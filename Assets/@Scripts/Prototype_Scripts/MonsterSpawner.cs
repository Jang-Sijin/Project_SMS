using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 몬스터 프리팹
    public float spawnInterval = 5f; // 몬스터 생성 주기 (초)
    public int monsterCount = 3; // 한 번에 생성되는 몬스터 수
    public float spawnDistance = 10f; // 플레이어로부터 몬스터 생성 거리
    public int maxMonsters = 20; // 최대 몬스터 수

    private Transform player;
    private List<GameObject> monsters = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (monsters.Count < maxMonsters)
            {
                for (int i = 0; i < monsterCount; i++)
                {
                    if (monsters.Count >= maxMonsters) break;

                    Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnDistance;
                    GameObject newMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                    monsters.Add(newMonster);
                    newMonster.GetComponent<Monster>().spawner = this;
                }
            }
        }
    }

    public void RemoveMonster(GameObject monster)
    {
        monsters.Remove(monster);
    }
}