using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameClear : MonoBehaviour
{
    public void OnClickNewGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickExitGame()
    {
        //게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
