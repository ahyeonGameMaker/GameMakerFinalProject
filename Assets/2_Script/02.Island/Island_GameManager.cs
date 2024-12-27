using System.Collections.Generic;
using UnityEngine;

public class Island_GameManager : MonoBehaviour
{
    public static Island_GameManager instance; // 싱글톤 패턴
    public List<GameObject> targetObjects; // 감지할 오브젝트 목록
    public GameObject gameOverUI; // 게임 오버 UI 오브젝트

    private bool isGameOver = false; // 게임 오버 상태 플래그
    public Island_SceneManager island_SceneManager;
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

        // 감지 대상 목록에 비어 있는 항목이 있으면 경고 출력
        if (targetObjects == null || targetObjects.Count == 0)
        {
            Debug.LogWarning("감지할 대상 오브젝트가 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        if (targetObjects[0] == null) // 게임 클리어
        {
            GameOver();
            island_SceneManager.clear = true;
            return; 
        }
        if (targetObjects[1] == null) // 게임 오버
        {
            GameOver();
            island_SceneManager.clear = false;
            return; 
        }
    }


    public void GameOver()
    {
        isGameOver = true; 

        // 게임 오버 UI 활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Debug.Log("게임 오버!");
    }
}
