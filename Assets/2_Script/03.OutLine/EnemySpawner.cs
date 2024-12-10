using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy; // ������ �� ������ �迭
    public GameObject[] enemySpawns; // ���� ��ġ �迭

    public float spawnTime = 5f; // �ʱ� ���� ����
    public float minSpawnTime = 1f; // �ּ� ���� ����
    public float spawnTimeReductionRate = 0.1f; // �ð� ���ҷ� (�� ����)

    private bool canSpawn = false;

    private float timer; // ���� Ÿ�̸�

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timer = spawnTime; // �ʱ�ȭ
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
            SpawnEnemy(); // �� ����
            timer = spawnTime; // Ÿ�̸� �ʱ�ȭ

            // ���� ���� ���� ����
            spawnTime = Mathf.Max(minSpawnTime, spawnTime - spawnTimeReductionRate);
        }
    }

    private void SpawnEnemy()
    {
        // ������ �� ������ ����
        GameObject randomEnemy = enemy[Random.Range(0, enemy.Length)];

        // ������ ���� ��ġ ����
        GameObject randomSpawnPoint = enemySpawns[Random.Range(0, enemySpawns.Length)];

        // �� ����
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
