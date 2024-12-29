using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopBarManager : MonoBehaviour
{
    private static TopBarManager instance;
    public ButtonHoverDOTween[] buttons;
    public List<SceneData> scenes = new List<SceneData>();
    public Image clearSceneImage;
    //private Image[] butonImages;
    public static TopBarManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        buttons = gameObject.GetComponentsInChildren<ButtonHoverDOTween>();
        gameObject.SetActive(false);
    }

    public void EndGame(int sceneNumber)
    {
        ButtonHoverDOTween buttonsDOTween = buttons[sceneNumber].GetComponent<ButtonHoverDOTween>();
        buttonsDOTween.clearImage.gameObject.SetActive(true);
        buttonsDOTween.OnPointerExit(null);
        buttonsDOTween.enabled = false;
        buttons[sceneNumber].GetComponent<Button>().enabled = false;
        SceneData sceneData = scenes[sceneNumber];
        sceneData.isClear = true;
    }

    public void LoadScene(int sceneNumber)
    {
        if (scenes[sceneNumber].isClear)
        {
            clearSceneImage.gameObject.SetActive(true);
            return;
        }
        clearSceneImage.gameObject.SetActive(false);
        SceneData sceneData = scenes[sceneNumber];
        if (!sceneData.isClear)
        {
            SceneManager.LoadScene(sceneData.sceneName);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetSceneButton((i == sceneNumber) ? true : false);
        }
        if (sceneNumber == 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public bool isClear;
}
