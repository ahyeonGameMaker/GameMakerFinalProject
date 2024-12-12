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
                    minute--;
                    second = 59;
                }
            }
        }

        timerText.text = string.Format("{0:00}:{1:00}", minute, second);

        if (minute == 0 && second == 0)
        {
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targettingRange);

    }

}
