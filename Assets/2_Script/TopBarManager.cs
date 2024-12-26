using System.Collections.Generic;
using UnityEditor.SearchService;
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
    }

    public void EndGame(int sceneNumber)
    {
        SceneData sceneData = scenes[sceneNumber - 1];
        sceneData.isClear = true;
    }

    public void LoadScene(int sceneNumber)
    {
        SceneData sceneData = scenes[sceneNumber - 1];
        if (!sceneData.isClear)
        {
            SceneManager.LoadScene(sceneData.sceneName);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetSceneButton((i == sceneNumber - 1) ? true : false);
        }

    }
}

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public bool isClear;
}