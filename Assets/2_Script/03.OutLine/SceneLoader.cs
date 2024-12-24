using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void NextGameScene()
    {
		SceneManager.LoadScene("02.Island");
	}

	public void Restart()
    {
        SceneManager.LoadScene("03.OutLine");
    }
}
