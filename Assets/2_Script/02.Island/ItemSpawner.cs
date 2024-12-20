using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // ����߸� ������ ������
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // ���� ���� ũ�� (����, ����)
    public float minSpawnInterval = 2f; // �ּ� ��ȯ ����
    public float maxSpawnInterval = 5f; // �ִ� ��ȯ ����

    private bool isSpawning = false; // ���� ���� Ȯ��

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
            // ���� ��� �ð� ���
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);

            // ���� ��ġ ����
            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(randomX, randomY);

            // ������ ����
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
