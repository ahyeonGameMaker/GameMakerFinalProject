using KoreanTyper;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public float duration = 5f; // 0���� 1�� �����ϴ� �� �ɸ��� �ð� (��)
    public Text TestText;
    public GameObject Panel;

    private string[] scripts = {
        "��ȸ ������ ������ ��!��Ծ��� ��\n��ȸ ������ �ִ� �������� ��� ���ϰ� ��ȸ ����� ��������!",
        "��ȸ�� �Ϻ��� ��ǰ�� �����Ϸ��� ����� �ʼ�!\n���� ���� �������� �ǰ��� �������� ���� ���� ������� �̲���!",
        "������ ����� ���鼭 ����� ��Ʈ������ ������ ���׵��� ����\n�츮�� ������ ��ȸ���� ������ �� �ִ� ���Ϸ� �������!"
    };

    private void Start()
    {
        StartCoroutine(CountToOne());
    }


    IEnumerator CountToOne()
    {
        for (int index = 0; index < scripts.Length; index++)
        {
            float elapsedTime = 0f;

            Panel.SetActive(true);
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
            Panel.SetActive(false);
            GameManager05.Instance.ScriptTime = false;

            //GamePlayTime
            yield return new WaitForSeconds(2);  //TODO : ���� �Ϸ� �� 10���� ����!
        }
    }

    private void OnTestSliderChange(int index, float currentValue)
    {
        TestText.text = scripts[index].Typing(currentValue);
    }
}
