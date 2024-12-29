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
			"�Բ� �� 1��, �׸��� �������� �츮.",
			"������ ���ο� ������ ���",
			"���� ����� ������ �׸��� �ž�",
			"�츮�� ���� ����, �츮�� ���� �߾�",
			"�������� �ϳ��� �츮���� �̾߱⸦ �̴ϰ������� �Ұ��Ҳ�",
			"ȭ���� Ŭ���Ͽ� ������ ��������!"};

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
        TopBarManager.Instance.gameObject.SetActive(true);

		for(int i = 0; i < TopBarManager.Instance.scenes.Count; i++)
		{
			if (!TopBarManager.Instance.scenes[i + 1].isClear)
			{
				TopBarManager.Instance.LoadScene(i + 1);
				break;
            }
		}
	}
}
