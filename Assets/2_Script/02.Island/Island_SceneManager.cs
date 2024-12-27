using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Island_SceneManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void ReLoadScene()
    {
        TopBarManager.Instance.LoadScene(4);
        SceneManager.LoadScene("02.Island");
    }

    public void NextScene()
    {
        TopBarManager.Instance.LoadScene(5);
        SceneManager.LoadScene("06.Warrior");
    }
}
