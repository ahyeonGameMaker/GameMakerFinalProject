using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
	public float duration = 5f; // 0에서 1로 증가하는 데 걸리는 시간 (초)
	public Text TestText;
	public GameObject Panel;

	public string[] scripts = new string[]{
		"대회 마감과 마주한 아!까먹었다 조\n대회 마감이 주는 공포심을 모두 피하고 대회 우승을 차지하자!",
		"대회에 완벽한 작품을 제출하려면 밤샘은 필수!\n밤을 새서 몰려오는 피곤함 페이저를 피해 팀을 우승으로 이끌자!",
		"게임을 만들거 가면서 생기는 스트레스와 수많은 버그들을 피해\n우리를 안전히 대회에서 제출할 수 있는 파일로 만들어줘!",
		"점점 제출 기한이 다가올수록 밀려오는 두려움 때문에\n도로는 3차선이 됐어.\n아!까먹었다 조의 멘탈을 위해 조금 더 조심해 줘!"
	};

	//private float currentValue = 0f; // 문장 글자 표현 정도
	private Coroutine currentCoroutine;

	private void Start()
	{
		StartOrRestartCoroutine();
	}

	public void StartOrRestartCoroutine()
	{
		if (currentCoroutine != null) StopCoroutine(currentCoroutine);

		currentCoroutine = StartCoroutine(CountToOne());
	}


	IEnumerator CountToOne()
	{
		for (int index = 0; index < scripts.Length; index++)
		{
			float elapsedTime = 0f;
			float currentValue = 0f;

			Panel.SetActive(true);
			GameManager05.Instance.ScriptTime = true;
			while (elapsedTime < duration)
			{
				currentValue = Mathf.Lerp(0f, 1f, elapsedTime / duration);
				elapsedTime += Time.deltaTime;
				OnTestSliderChange(index, currentValue);
				yield return null;
			}
			currentValue = 1f;
			OnTestSliderChange(index, currentValue);

			yield return new WaitForSeconds(1);
			Panel.SetActive(false);
			GameManager05.Instance.ScriptTime = false;

			//GamePlayTime
			yield return new WaitForSeconds(10);
		}
	}

	private void OnTestSliderChange(int index, float currentValue)
	{
		TestText.text = scripts[index].Typing(currentValue);
	}
}
