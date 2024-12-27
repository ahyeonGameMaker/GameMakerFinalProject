using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager05 : MonoBehaviour
{
    public static GameManager05 Instance;
	public Image HpBar;
	public TextMeshProUGUI ScoreTMP;

	public bool ScriptTime;
	public int Score = 0;
	private int TotalScore = 20; 
	public bool GameClear = false;

	public AudioSource AudioSource;
	public AudioClip ProjectileExplosionClip;
	public AudioClip FinalSoundClip;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
        Score = Score < 0 ? 0 : Score;
        Score = Score > TotalScore ? TotalScore : Score;
        float amount = (float)Score / (float)TotalScore;
		if (amount == 1)
		{
			GameClear = true;
        }

		HpBar.fillAmount = amount;
		ScoreTMP.text = $"{Score}/{TotalScore}";
	}

	public void NextScene()
	{
		TopBarManager.Instance.EndGame(2);
        TopBarManager.Instance.LoadScene(3);
	}

	public void ProjectileExplosion()
	{
		AudioSource.PlayOneShot(ProjectileExplosionClip);
	}

	public void FinalSound()
	{
		AudioSource.PlayOneShot(FinalSoundClip);
	}
}
