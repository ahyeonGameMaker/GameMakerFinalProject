using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject quitButton;

    void Start()
    {
        // VideoPlayer 재생 완료 이벤트에 메소드 등록
        videoPlayer.loopPointReached += OnVideoEnd;

        // 버튼 비활성화 (초기 상태)
        quitButton.SetActive(false);
    }

    // 재생 완료 시 호출될 메소드
    void OnVideoEnd(VideoPlayer vp)
    {
        // 버튼 활성화
        quitButton.SetActive(true);
    }

    // 종료 버튼 클릭 시 호출될 메소드
    public void QuitGame()
    {
        // 유니티 에디터에서는 게임 정지, 빌드에서는 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
