using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager05 : MonoBehaviour
{
    public static GameManager05 Instance;
	public Image HpBar;
	public TextMeshProUGUI ScoreTMP;

	public bool ScriptTime;
	public int Score = 0;
	private int TotalScore = 20; //TODO 20;
	public bool GameClear = false;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
        Score = Score < 0 ? 0 : Score;
        Score = Score > TotalScore ? TotalScore : Score;
        float amount = (float)Score / (float)TotalScore;
		if (amount == 1)
		{
			amount = 1;
			GameClear = true;
			Debug.Log("GAME CLEAR!!!!!!!!!!!!!!!!!");
        }

		HpBar.fillAmount = amount;
		ScoreTMP.text = $"{Score}/{TotalScore}";
	}
}
