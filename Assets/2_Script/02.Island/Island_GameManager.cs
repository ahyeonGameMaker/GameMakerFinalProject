using System.Collections.Generic;
using UnityEngine;

public class Island_GameManager : MonoBehaviour
{
    public static Island_GameManager instance; // �̱��� ����
    public List<GameObject> targetObjects; // ������ ������Ʈ ���
    public GameObject gameOverUI; // ���� ���� UI ������Ʈ

    private bool isGameOver = false; // ���� ���� ���� �÷���

    void Awake()
    {
        // �̱��� ����
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
        // ���� ���� UI ��Ȱ��ȭ
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // ���� ��� ��Ͽ� ��� �ִ� �׸��� ������ ��� ���
        if (targetObjects == null || targetObjects.Count == 0)
        {
            Debug.LogWarning("������ ��� ������Ʈ�� �������� �ʾҽ��ϴ�.");
        }
    }

    void Update()
    {
        // ���� ��� ������Ʈ üũ
        for (int i = targetObjects.Count - 1; i >= 0; i--)
        {
            if (targetObjects[i] == null) // ������Ʈ�� ����� ���
            {
                GameOver(); // ��� ���� ����
                return; // �ߺ� ȣ�� ����
            }
        }
    }


    public void GameOver()
    {
        isGameOver = true; // ���� ���� ���� ����

        // ���� ���� UI Ȱ��ȭ
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Debug.Log("���� ����!");
    }
}
