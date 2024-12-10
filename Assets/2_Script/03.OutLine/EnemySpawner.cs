using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy; // 스폰할 적 프리팹 배열
    public GameObject[] enemySpawns; // 스폰 위치 배열

    public float spawnTime = 5f; // 초기 스폰 간격
    public float minSpawnTime = 1f; // 최소 스폰 간격
    public float spawnTimeReductionRate = 0.1f; // 시간 감소량 (초 단위)

    private bool canSpawn = false;

    private float timer; // 스폰 타이머

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timer = spawnTime; // 초기화
    }

    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy(); // 적 스폰
            timer = spawnTime; // 타이머 초기화

            // 스폰 간격 점점 단축
            spawnTime = Mathf.Max(minSpawnTime, spawnTime - spawnTimeReductionRate);
        }
    }

    private void SpawnEnemy()
    {
        // 랜덤한 적 프리팹 선택
        GameObject randomEnemy = enemy[Random.Range(0, enemy.Length)];

        // 랜덤한 스폰 위치 선택
        GameObject randomSpawnPoint = enemySpawns[Random.Range(0, enemySpawns.Length)];

        // 적 생성
        Instantiate(randomEnemy, randomSpawnPoint.transform.position, Quaternion.identity);
    }

    public void GameOver()
    {
        canSpawn = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
