using System.Collections.Generic;
using UnityEngine;

public class Island_GameManager : MonoBehaviour
{
    public static Island_GameManager instance; // 싱글톤 패턴
    public List<GameObject> targetObjects; // 감지할 오브젝트 목록
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

        // 감지 대상 목록에 비어 있는 항목이 있으면 경고 출력
        if (targetObjects == null || targetObjects.Count == 0)
        {
            Debug.LogWarning("감지할 대상 오브젝트가 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        // 감지 대상 오브젝트 체크
        for (int i = targetObjects.Count - 1; i >= 0; i--)
        {
            if (targetObjects[i] == null) // 오브젝트가 사라진 경우
            {
                GameOver(); // 즉시 게임 오버
                return; // 중복 호출 방지
            }
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
