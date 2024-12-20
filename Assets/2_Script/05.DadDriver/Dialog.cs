using KoreanTyper;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public float duration = 5f; // 0에서 1로 증가하는 데 걸리는 시간 (초)
    public Text TestText;
    public GameObject DialogPanel;
    public GameObject EndingPhoto;
    public Animator animator;
    private bool ScriptDisplayDone = false;
    private Coroutine scriptDisplayCoroutine;


	private string[] scripts = {
        "대회 마감과 마주한 아!까먹었다 조\n대회 마감이 주는 공포심을 모두 피하고 대회 우승을 차지하자!",
        "대회에 완벽한 작품을 제출하려면 밤샘은 필수!\n밤을 새서 몰려오는 피곤함 페이저를 피해 팀을 우승으로 이끌자!",
        "게임을 만들어 가면서 생기는 스트레스와 수많은 버그들을 피해\n우리를 안전히 대회에서 제출할 수 있는 파일로 만들어줘!"
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
