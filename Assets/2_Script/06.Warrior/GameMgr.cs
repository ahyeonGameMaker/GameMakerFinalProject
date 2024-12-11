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
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targettingRange);
    }

}
