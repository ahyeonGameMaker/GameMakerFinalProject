using TMPro;
using UnityEngine;
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

    private static GameMgr instance;

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
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targettingRange);

    }

}
