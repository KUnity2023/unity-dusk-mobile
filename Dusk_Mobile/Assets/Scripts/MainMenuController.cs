using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //StartBtn누를 시 띄울 패널
    public GameObject uiTutorialSelectPanel;
    
    // MainMenuScene에서 Click버튼 누를시 Tutorial Skip여부 묻는 UI 활성화
    public void OnStartClick()
    {
        //로딩 씬 진입.
        uiTutorialSelectPanel.SetActive(true);
    }
    //Tutorial 실행
    public void OnNewStartClick()
    {
        SceneManagerEX.Instance.LoadTutorial();
    }
    //Tutorial 안한다고 클릭, 바로 캐선창으로
    public void OnCancelClick()
    {
        SceneManagerEX.Instance.LoadCharacterSelect();
    }
    public void OnExitClick()
    {
        SceneManagerEX.Instance.ExitGame();
    }
}
