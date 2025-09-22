#if UNITY_EDITOR
    using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour 
{

    public AudioSource music;

    // 게임 재시작
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        TimeScale(1);
    }

    // 게임 종료
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
        Application.Quit();
    }

    public void TimeScale(int scale)
    {
        Time.timeScale = scale;
    switch (scale)
    {
        case 0:
            music.Pause();
            break;
        case 1:
            music.Play();
            break;
    }
    }
}
