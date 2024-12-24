using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // 떨어뜨릴 아이템 프리팹
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // 스폰 영역 크기 (가로, 세로)
    public float minSpawnInterval = 2f; // 최소 소환 간격
    public float maxSpawnInterval = 5f; // 최대 소환 간격

    private bool isSpawning = false; // 스폰 상태 확인

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnItems());
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private System.Collections.IEnumerator SpawnItems()
    {
        while (isSpawning)
        {
            // 랜덤 대기 시간 계산
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);

            // 스폰 위치 생성
            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(randomX, randomY);

            // 아이템 생성
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 스폰 영역 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
