using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void NextGameScene()
    {
        if (UIManager.instance.clear == true)
            TopBarManager.Instance.EndGame(3);
        TopBarManager.Instance.LoadScene(4);
	}

	public void Restart()
    {
        TopBarManager.Instance.LoadScene(3);
    }
}
