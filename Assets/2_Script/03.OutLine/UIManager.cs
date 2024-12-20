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
        scoreText.text = "고마워 총" + ScoreManager.instance.score + "개의 버그를 잡았구나! 수고했어";
        gameOverPanel.SetActive(true);
    }
}
