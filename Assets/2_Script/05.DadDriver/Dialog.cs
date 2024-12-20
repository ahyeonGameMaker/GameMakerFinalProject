using KoreanTyper;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public float duration = 5f; // 0���� 1�� �����ϴ� �� �ɸ��� �ð� (��)
    public Text TestText;
    public GameObject DialogPanel;
    public GameObject EndingPhoto;
    public Animator animator;
    private bool ScriptDisplayDone = false;
    private Coroutine scriptDisplayCoroutine;


	private string[] scripts = {
        "��ȸ ������ ������ ��!��Ծ��� ��\n��ȸ ������ �ִ� �������� ��� ���ϰ� ��ȸ ����� ��������!",
        "��ȸ�� �Ϻ��� ��ǰ�� �����Ϸ��� ����� �ʼ�!\n���� ���� �������� �ǰ��� �������� ���� ���� ������� �̲���!",
        "������ ����� ���鼭 ����� ��Ʈ������ ������ ���׵��� ����\n�츮�� ������ ��ȸ���� ������ �� �ִ� ���Ϸ� �������!"
    };

    private void Start()
    {
        EndingPhoto.SetActive(false);
        DialogPanel.SetActive(false);

		scriptDisplayCoroutine = StartCoroutine(ScriptDisplay());
    }

    private void Update()
    {
		if (GameManager05.Instance.GameClear)
        {
			ScriptDisplayDone = true;
            StopCoroutine(scriptDisplayCoroutine);
		}

		if (GameManager05.Instance.GameClear && !EndingPhoto.activeSelf && ScriptDisplayDone)
        {
            EndingPhoto.SetActive(true);
            animator.SetTrigger("End");
            GameManager05.Instance.FinalSound();
        }
    }

    IEnumerator ScriptDisplay()
    {
        for (int index = 0; index < scripts.Length; index++)
        {
            float elapsedTime = 0f;

            DialogPanel.SetActive(true);
            GameManager05.Instance.ScriptTime = true;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float currentValue = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                OnTestSliderChange(index, currentValue);
                yield return null;
            }
            OnTestSliderChange(index, 1);

            yield return new WaitForSeconds(1);
            DialogPanel.SetActive(false);
            GameManager05.Instance.ScriptTime = false;

            //GamePlayTime
            yield return new WaitForSeconds(10);
        }
        ScriptDisplayDone = true;
    }

    private void OnTestSliderChange(int index, float currentValue)
    {
        TestText.text = scripts[index].Typing(currentValue);
    }
}
