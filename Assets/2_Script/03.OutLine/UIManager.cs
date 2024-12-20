using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText;
    public GameObject nextBtn;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GaemOverUI()
    {
        scoreText.text = "���� ��" + ScoreManager.instance.score + "���� ���׸� ��ұ���! �����߾�";
        gameOverPanel.SetActive(true);
    }
}
