using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    public string beforeSceneName;
    public GameObject selectChar;
    public GameObject[] CharacterPrefab;
    public int[] HK_Start_Stat = { 3, 3, 0, 0 };
    public int[] King_Start_Stat = { 2, 2, 1, 1 };
    public int[] Women_Start_Stat = { 0, 1, 1, 4 };
    public int[] max_Status = { 5, 5, 5, 5 };

    // 유일한 인스턴스를 저장할 정적 변수
    private static SceneManagerEX instance;
    

    // 다른 스크립트에서 접근할 때 사용할 프로퍼티
    public static SceneManagerEX Instance
    {
        get
        {
            // 인스턴스가 없으면 생성
            if (instance == null)
            {
                instance = FindObjectOfType<SceneManagerEX>();

                // Scene에 없으면 새로 생성
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SceneManagerEX");
                    instance = singletonObject.AddComponent<SceneManagerEX>();
                }
            }

            // 이미 존재하는 경우 기존 인스턴스 반환
            return instance;
        }
    }

    // 싱글톤의 기능을 구현할 메서드들은 아래에 추가

    private void Awake()
    {
        // 유일한 인스턴스가 여러 개 생성되지 않도록 처리
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    //스테이지1 이동 스크립트
    public void LoadStage1()
    {
        beforeSceneName = "CharacterSelect";
        SceneManager.LoadScene("Loading");
    }

    //캐선창 스크립트
    public void LoadCharacterSelect()
    {
        beforeSceneName = "MainMenu";       
        SceneManager.LoadScene("CharacterSelect");
    }
    //Tutorial Scene
    public void LoadTutorial()
    {
        beforeSceneName = "MainMenu";
        SceneManager.LoadScene("Loading");
    }

    public void LoadStage2()
    {
        beforeSceneName = "Stage1";
        SceneManager.LoadScene("Loading");
    }
}
