using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        //�ε� �� ����.
        SceneManagerEX.Instance.LoadTutorial();
    }

    public void OnExitClick()
    {
        //���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
