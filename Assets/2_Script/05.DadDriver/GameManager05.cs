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
	private float TotalScore = 20.0f;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		float amount = Score / TotalScore;
		amount = amount > 1 ? 1 : amount;	//¼º°ø
		amount = amount < 0 ? 0 : amount;   //DEAD

		Debug.Log("FillAmount " + amount);
		HpBar.fillAmount = amount;
	}
}
