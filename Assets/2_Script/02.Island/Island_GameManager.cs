using UnityEngine;

public class Island_GameManager : MonoBehaviour
{
    public static Island_GameManager instance; // �̱��� ����
    public GameObject enemyPortal; // Enemy Portal ������Ʈ
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

        // Enemy Portal�� �������� �ʾ��� ��� ��� ���
        if (enemyPortal == null)
        {
            Debug.LogError("Enemy Portal�� �������� �ʾҽ��ϴ�.");
        }
    }

    void Update()
    {
        // Enemy Portal�� �ı��Ǿ����� Ȯ��
        if (enemyPortal == null && !isGameOver)
        {
            GameOver();
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
