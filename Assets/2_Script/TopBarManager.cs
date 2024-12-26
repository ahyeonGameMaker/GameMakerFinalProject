using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopBarManager : MonoBehaviour
{
    private static TopBarManager instance;
	private ButtonHoverDOTween[] buttons;
	private Image[] butonImages;

	private void Awake()
	{
		if (instance == null)
        {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else
        {
            Destroy(gameObject);
        }
	}

	private void Start()
	{
		buttons = gameObject.GetComponentsInChildren<ButtonHoverDOTween>();
		butonImages = gameObject.GetComponentsInChildren<Image>();
	}

	public void LoadScene(int SceneNumber)
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			butonImages[i+1].sprite = buttons[i].OffSprite;
		}
		butonImages[SceneNumber].sprite = buttons[SceneNumber - 1].OnSprite;

		if (SceneNumber == 1) { SceneManager.LoadScene("04.DollMaker"); }
		else if (SceneNumber == 2) { SceneManager.LoadScene("05.DadDriver"); }
		else if (SceneNumber == 3) { SceneManager.LoadScene("03.OutLine"); }
		else if (SceneNumber == 4) { SceneManager.LoadScene("01.Stella"); }
		else if (SceneNumber == 5) { SceneManager.LoadScene("06.Warrior"); }
		else if (SceneNumber == 6) { SceneManager.LoadScene("01.Stella"); }
	}
}
