using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab; // 생성할 적 프리팹
    public Transform spawnPoint; // 적이 생성될 위치
    public Transform targetBase; // 적이 목표로 삼는 기지 Transform
    public float spawnInterval = 1.5f; // 적 생성 간격
    public int[] enemiesPerWave; // 각 웨이브의 적 수 배열

    private int currentWave = 0; // 현재 웨이브
    private int enemiesRemaining; // 현재 웨이브에서 남아있는 적 수
    private bool waveInProgress = false; // 웨이브 진행 상태

    void Start()
    {
        StartNextWave(); // 첫 번째 웨이브 시작
    }

    void Update()
    {
        // 현재 웨이브가 끝났고 남아있는 적이 없으면 다음 웨이브 시작
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
            enemiesRemaining = enemiesPerWave[currentWave - 1]; // 현재 웨이브의 적 수
            waveInProgress = true;
            StartCoroutine(SpawnWave());
        }
        else
        {
            Debug.Log("모든 웨이브가 끝났습니다!");
            // 게임 종료 로직 추가 가능
        }
    }

    IEnumerator SpawnWave()
    {
        Debug.Log($"웨이브 {currentWave} 시작!");
        for (int i = 0; i < enemiesRemaining; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // 적 생성 간격 대기
        }
        waveInProgress = false; // 모든 적 생성 완료
    }

    void SpawnEnemy()
    {
        // 적 생성
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // 생성된 적의 EnemyMovement 스크립트에 targetBase 설정
        var enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.targetBase = targetBase; // targetBase는 WaveManager에서 설정된 Transform
        }

        // 적의 Health에 사망 이벤트 연결
        var health = enemy.GetComponent<Enemy>();
        if (health != null)
        {
            health.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    void HandleEnemyDeath()
    {
        enemiesRemaining--;
        Debug.Log($"적이 죽음. 남은 적: {enemiesRemaining}");

        if (enemiesRemaining <= 0 && !waveInProgress)
        {
            Debug.Log("웨이브 클리어!");
        }
    }
}
