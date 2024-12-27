using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject quitButton;

    void Start()
    {
        // VideoPlayer ��� �Ϸ� �̺�Ʈ�� �޼ҵ� ���
        videoPlayer.loopPointReached += OnVideoEnd;

        // ��ư ��Ȱ��ȭ (�ʱ� ����)
        quitButton.SetActive(false);
    }

    // ��� �Ϸ� �� ȣ��� �޼ҵ�
    void OnVideoEnd(VideoPlayer vp)
    {
        // ��ư Ȱ��ȭ
        quitButton.SetActive(true);
    }

    // ���� ��ư Ŭ�� �� ȣ��� �޼ҵ�
    public void QuitGame()
    {
        // ����Ƽ �����Ϳ����� ���� ����, ���忡���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
