using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �� ������
    public Transform spawnPoint; // ���� ������ ��ġ
    public Transform targetBase; // ���� ��ǥ�� ��� ���� Transform
    public float spawnInterval = 1.5f; // �� ���� ����
    public int[] enemiesPerWave; // �� ���̺��� �� �� �迭

    private int currentWave = 0; // ���� ���̺�
    private int enemiesRemaining; // ���� ���̺꿡�� �����ִ� �� ��
    private bool waveInProgress = false; // ���̺� ���� ����

    void Start()
    {
        StartNextWave(); // ù ��° ���̺� ����
    }

    void Update()
    {
        // ���� ���̺갡 ������ �����ִ� ���� ������ ���� ���̺� ����
        if (!waveInProgress && enemiesRemaining <= 0)
        {
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        if (currentWave < enemiesPerWave.Length)
        {
            currentWave++;
            enemiesRemaining = enemiesPerWave[currentWave - 1]; // ���� ���̺��� �� ��
            waveInProgress = true;
            StartCoroutine(SpawnWave());
        }
        else
        {
            Debug.Log("��� ���̺갡 �������ϴ�!");
            // ���� ���� ���� �߰� ����
        }
    }

    IEnumerator SpawnWave()
    {
        Debug.Log($"���̺� {currentWave} ����!");
        for (int i = 0; i < enemiesRemaining; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // �� ���� ���� ���
        }
        waveInProgress = false; // ��� �� ���� �Ϸ�
    }

    void SpawnEnemy()
    {
        // �� ����
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // ������ ���� EnemyMovement ��ũ��Ʈ�� targetBase ����
        var enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.targetBase = targetBase; // targetBase�� WaveManager���� ������ Transform
        }

        // ���� Health�� ��� �̺�Ʈ ����
        var health = enemy.GetComponent<Enemy>();
        if (health != null)
        {
            health.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    void HandleEnemyDeath()
    {
        enemiesRemaining--;
        Debug.Log($"���� ����. ���� ��: {enemiesRemaining}");

        if (enemiesRemaining <= 0 && !waveInProgress)
        {
            Debug.Log("���̺� Ŭ����!");
        }
    }
}
