using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        //로딩 씬 진입.
        SceneManagerEX.Instance.LoadTutorial();
    }

    public void OnExitClick()
    {
        //게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
