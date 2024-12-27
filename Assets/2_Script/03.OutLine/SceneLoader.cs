using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void NextGameScene()
    {
        TopBarManager.Instance.LoadScene(4);
        SceneManager.LoadScene("02.Island");
	}

	public void Restart()
    {
        TopBarManager.Instance.LoadScene(3);
        SceneManager.LoadScene("03.OutLine");
    }
}
