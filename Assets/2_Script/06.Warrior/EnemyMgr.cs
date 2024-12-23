using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : MonoBehaviour
{
    public Unit[] units;
    public List<Unit> enemiesPoolings = new List<Unit>();
    public WaveInfo[] waveInfos;
    public Unit boss;
    private static EnemyMgr instance;
    public static EnemyMgr Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
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
                Unit enemy = Instantiate(units[i]);
                enemiesPoolings.Add(enemy);
                return enemy;
            }
        }
        return null;
    }

    IEnumerator CoSpawnEnemy()
    {
        bool bossSpawned = false;
        while (0 < waveInfos[GameMgr.Instance.waveLevel].spawnTime)
        {
            if (waveInfos[GameMgr.Instance.waveLevel].bossWave && !bossSpawned)
            {
                if (boss != null && !boss.gameObject.activeSelf)
                {
                    boss.gameObject.SetActive(true);
                    bossSpawned = true;
                }
            }
            float totalChance = 0f;
            foreach (var spawn in waveInfos[GameMgr.Instance.waveLevel].enemySpawns)
            {
                totalChance += spawn.chance;
            }

            float random = Random.Range(0f, totalChance);

            float cumulativeChance = 0f;
            for (int i = 0; i < waveInfos[GameMgr.Instance.waveLevel].enemySpawns.Length; i++)
            {
                cumulativeChance += waveInfos[GameMgr.Instance.waveLevel].enemySpawns[i].chance;

                if (random <= cumulativeChance)
                {
                    Unit enemy = GetEnemy(waveInfos[GameMgr.Instance.waveLevel].enemySpawns[i].type);
                    if (enemy != null)
                    {
                        enemy.transform.position = GameMgr.Instance.enemySpawnPoint.transform.position;
                        if (enemy.enemyType == EnemyType.Skeleton || enemy.enemyType == EnemyType.Goblin)
                        {
                            enemy.transform.position = new Vector3(enemy.transform.position.x, -2.942f, enemy.transform.position.z);
                        }else if(enemy.enemyType == EnemyType.IceGolem)
                        {
                            enemy.transform.position = new Vector3(enemy.transform.position.x, -1.55f, enemy.transform.position.z);
                        }else if(enemy.enemyType == EnemyType.Reptilian)
                        {
                            enemy.transform.position = new Vector3(enemy.transform.position.x, -2.13f, enemy.transform.position.z);
                        }
                    }
                    break;
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
    public bool bossWave;
}
[System.Serializable]
public class EnemySpawn
{
    public EnemyType type;
    public float chance;
}

public enum EnemyType
{
    Skeleton,
    Goblin, 
    IceGolem,
    Reptilian,
    RockMonster
}



