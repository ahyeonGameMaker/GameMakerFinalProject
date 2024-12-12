using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : MonoBehaviour
{
    public Unit[] units;
    public List<Unit> enemiesPoolings = new List<Unit>();
    public WaveInfo[] waveInfos;

    private void Start()
    {
        StartCoroutine(CoSpawnEnemy());
    }

    public Unit GetEnemy(EnemyType type)
    {
        for (int i = 0; i < enemiesPoolings.Count; i++)
        {
            if (!enemiesPoolings[i].gameObject.activeSelf && enemiesPoolings[i].enemyType == type)
            {
                enemiesPoolings[i].gameObject.SetActive(true);
                return enemiesPoolings[i];
            }
        }

        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].enemyType == type)
            {
                Unit ballEnemy = Instantiate(units[i]);
                enemiesPoolings.Add(ballEnemy);
                return ballEnemy;
            }
        }
        return null;
    }

    IEnumerator CoSpawnEnemy()
    {
        while(0 < waveInfos[GameMgr.Instance.waveLevel].spawnTime)
        {
            for (int i = 0; i < waveInfos[GameMgr.Instance.waveLevel].enemySpawns.Length; i++)
            {
                float random = Random.Range(0f, 100f);
                if (random <= waveInfos[GameMgr.Instance.waveLevel].enemySpawns[i].chance)
                {
                    Unit enemy = GetEnemy(waveInfos[GameMgr.Instance.waveLevel].enemySpawns[i].type);
                    enemy.transform.position = GameMgr.Instance.enemySpawnPoint.transform.position;
                    if (enemy.enemyType == EnemyType.Skeleton)
                    {
                        enemy.transform.position = new Vector3(enemy.transform.position.x, -2.942f, enemy.transform.position.z);
                    }
                }
            }

            yield return new WaitForSeconds(waveInfos[GameMgr.Instance.waveLevel].spawnTime);
        }
    }
}


[System.Serializable]
public class WaveInfo
{
    public EnemySpawn[] enemySpawns;
    public float spawnTime;
}
[System.Serializable]
public class EnemySpawn
{
    public EnemyType type;
    public float chance;
}

public enum EnemyType
{
    Skeleton
}



