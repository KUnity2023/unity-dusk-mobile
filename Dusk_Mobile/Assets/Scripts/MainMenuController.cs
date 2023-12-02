using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //StartBtn���� �� ��� �г�
    public GameObject uiTutorialSelectPanel;
    
    // MainMenuScene���� Click��ư ������ Tutorial Skip���� ���� UI Ȱ��ȭ
    public void OnStartClick()
    {
        //�ε� �� ����.
        uiTutorialSelectPanel.SetActive(true);
    }
    //Tutorial ����
    public void OnNewStartClick()
    {
        SceneManagerEX.Instance.LoadTutorial();
    }
    //Tutorial ���Ѵٰ� Ŭ��, �ٷ� ĳ��â����
    public void OnCancelClick()
    {
        SceneManagerEX.Instance.LoadCharacterSelect();
    }
    public void OnExitClick()
    {
        SceneManagerEX.Instance.ExitGame();
    }
}
