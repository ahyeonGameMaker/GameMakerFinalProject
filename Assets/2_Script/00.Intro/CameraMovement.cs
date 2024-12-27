using KoreanTyper;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
	public Text TestTexts;
	public GameObject BtnObj;
	public float moveSpeed = 2f;
	public TopBarManager topBarManager;

    private void Start()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 20f);

		StartCoroutine(TypingText());
	}

	void Update()
	{
		float moveZ = moveSpeed * Time.deltaTime;
		transform.Translate(0, 0, -moveZ);

		if (transform.position.z < -110)
		{
			BtnObj.SetActive(true);
		}
	}

	public IEnumerator TypingText()
	{
		string[] strings = new string[]{ 
			"함께 한 1년, 그리고 앞으로의 우리.",
			"매일이 새로운 도전과 배움",
			"웃고 떠들던 교실이 그리울 거야",
			"우리가 만든 게임, 우리가 만든 추억",
			"게임으로 하나된 우리들의 이야기를 미니게임으로 소개할께",
			"화면을 클릭하여 게임을 시작하자!"};

		TestTexts.text = "";
		for (int t = 0; t < strings.Length; t++)
		{
			int strTypingLength = strings[t].GetTypingLength();
			for (int i = 0; i <= strTypingLength; i++)
			{
				TestTexts.text = strings[t].Typing(i);
				yield return new WaitForSeconds(0.03f);
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public void NextScene()
	{
		topBarManager.gameObject.SetActive(true);

		TopBarManager.Instance.LoadScene(1);
        SceneManager.LoadScene("04.DollMaker");
	}
}
