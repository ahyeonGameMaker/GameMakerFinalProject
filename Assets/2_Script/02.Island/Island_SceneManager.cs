using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Island_SceneManager : MonoBehaviour
{
    public bool clear;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void ReLoadScene()
    {
        TopBarManager.Instance.LoadScene(4);
    }

    public void NextScene()
    {
        if (clear)
            TopBarManager.Instance.EndGame(4);
        TopBarManager.Instance.LoadScene(5);
    }
}
