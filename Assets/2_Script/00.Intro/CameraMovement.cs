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
		string[] strings = new string[3]{ "20XX년 X월 X일 X요일 오후 HH시 MM분",
											  "유니티 한글 타이퍼 오토 타이핑 데모 씬",
											  "이 데모는 자동으로 작성되고 있습니다." };

		TestTexts.text = "";
		for (int t = 0; t < strings.Length; t++)
		{
			int strTypingLength = strings[t].GetTypingLength();
			for (int i = 0; i <= strTypingLength; i++)
			{
				TestTexts.text = strings[t].Typing(i);
				yield return new WaitForSeconds(0.03f);
			}
			// Wait 1 second per 1 sentence | 한 문장마다 1초씩 대기
			yield return new WaitForSeconds(1f);
		}
	}

	public void NextScene()
	{
		SceneManager.LoadScene("04.DollMaker");
	}
}
