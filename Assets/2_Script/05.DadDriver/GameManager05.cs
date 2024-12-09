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
	private float totalScore = 20.0f;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		float amount = Score / totalScore + 0.5f;
		amount = amount > 1 ? 1.0f : amount;	//¼º°ø
		amount = amount < 0 ? 1 : amount;   //DEAD

		HpBar.fillAmount = amount;
	}
}
