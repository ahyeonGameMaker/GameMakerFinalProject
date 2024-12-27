using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public Sprite firstAttackSprite;
    public Sprite secondAttackSprite;
    public Sprite thirdAttackSprite;
    public Sprite idleSprite;
    public Image attackImage;

    public float targettingRange;

    public GameObject enemySpawnPoint;

    public WarriorPlayer player;

    public TMP_Text timerText;

    public int waveLevel;

    public int maxMinute;
    public int minute;
    public int second;
    float elapsedTime;

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    private static GameMgr instance;
    public Stone stone;
    bool stop;

    public static GameMgr Instance
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
        minute = maxMinute;
    }

    public void Update()
    {
        UpdateTimer();
    }

    public void NextScene(bool clear)
    {
        if (!stop)
        {
            if (clear)
            {
                gameClearPanel.SetActive(true);
            }
            else
            {
                gameOverPanel.SetActive(true);
            }
            StartCoroutine(CoNextScene());
        }
        stop = true;


    }

    IEnumerator CoNextScene()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2);
            TopBarManager.Instance.LoadScene(6);
            SceneManager.LoadScene("01.Stella");
        }
    }

    void UpdateTimer()
    {
        if (minute > 0 || second > 0)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f)
            {
                elapsedTime = 0f;
                if (second > 0)
                {
                    second--;
                }
                else if (minute > 0)
                {
                    if (minute < maxMinute)
                    {
                        if (waveLevel <= maxMinute)
                        {
                            waveLevel++;
                        }
                    }
                    minute--;
                    second = 59;
                }
            }
        }

        timerText.text = string.Format("{0:00}:{1:00}", minute, second);

        if (minute == 0 && second == 0)
        {
            stone.gameStop = true;
            player.TakeDamage(100000, null);
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targettingRange);

    }

}
