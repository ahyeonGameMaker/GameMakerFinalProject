using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
	public float duration = 5f; // 0���� 1�� �����ϴ� �� �ɸ��� �ð� (��)
	public Text TestText;
	public GameObject Panel;

	public string[] scripts = new string[]{
		"��ȸ ������ ������ ��!��Ծ��� ��\n��ȸ ������ �ִ� �������� ��� ���ϰ� ��ȸ ����� ��������!",
		"��ȸ�� �Ϻ��� ��ǰ�� �����Ϸ��� ����� �ʼ�!\n���� ���� �������� �ǰ��� �������� ���� ���� ������� �̲���!",
		"������ ����� ���鼭 ����� ��Ʈ������ ������ ���׵��� ����\n�츮�� ������ ��ȸ���� ������ �� �ִ� ���Ϸ� �������!",
		"���� ���� ������ �ٰ��ü��� �з����� �η��� ������\n���δ� 3������ �ƾ�.\n��!��Ծ��� ���� ��Ż�� ���� ���� �� ������ ��!"
	};

	//private float currentValue = 0f; // ���� ���� ǥ�� ����
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
