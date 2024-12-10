using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager05 : MonoBehaviour
{
    public static GameManager05 Instance;
	public Image HpBar;

	public bool ScriptTime;
	public int Score = 0;
	private float TotalScore = 1; //TODO 20.0f;
	public bool GameClear = false;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		float amount = Score / TotalScore;
		amount = amount < 0 ? 0 : amount;   //DEAD
		if (amount > 1)
		{
			amount = 1;
			GameClear = true;
			Debug.Log("GAME CLEAR!!!!!!!!!!!!!!!!!");
        }

		HpBar.fillAmount = amount;
	}
}
