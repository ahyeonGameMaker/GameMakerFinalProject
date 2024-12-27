using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopBarManager : MonoBehaviour
{
    private static TopBarManager instance;
    private ButtonHoverDOTween[] buttons;
    public List<SceneData> scenes = new List<SceneData>();
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
        SceneData sceneData = scenes[sceneNumber];
        sceneData.isClear = true;
    }

    public void LoadScene(int sceneNumber)
    {
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
