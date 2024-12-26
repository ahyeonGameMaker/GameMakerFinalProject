using UnityEngine;

public class Island_GameManager : MonoBehaviour
{
    public static Island_GameManager instance; // 싱글톤 패턴
    public GameObject enemyPortal; // Enemy Portal 오브젝트
    public GameObject gameOverUI; // 게임 오버 UI 오브젝트

    private bool isGameOver = false; // 게임 오버 상태 플래그

    void Awake()
    {
        // 싱글톤 설정
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 게임 오버 UI 비활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Enemy Portal이 설정되지 않았을 경우 경고 출력
        if (enemyPortal == null)
        {
            Debug.LogError("Enemy Portal이 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        // Enemy Portal이 파괴되었는지 확인
        if (enemyPortal == null && !isGameOver)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true; // 게임 오버 상태 설정

        // 게임 오버 UI 활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Debug.Log("게임 오버!");
    }
}
