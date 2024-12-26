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
        SceneManager.LoadScene("02.Island");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("06.Warrior");
    }
}
