using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms;

public class UI_GameClear : MonoBehaviour
{
    public TextMeshProUGUI clearTime;
    private void Awake()
    {
        clearTime.text = ((int)SceneManagerEX.Instance.totalTime / 60 % 60).ToString() + ":" + ((int)SceneManagerEX.Instance.totalTime % 60).ToString();
    }
    public void OnClickNewGame()
    {
        Time.timeScale = 1;
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
