using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    public string beforeSceneName;

    public GameObject selectChar;

    public GameObject[] CharacterPrefab;

    public float totalTime = 0f;

    public int[] HK_Start_Stat = { 3, 3, 0, 0 };
    public int[] King_Start_Stat = { 2, 2, 1, 1 };
    public int[] Women_Start_Stat = { 0, 1, 1, 4 };
    public int[] max_Status = { 5, 5, 5, 5 };
    [SerializeField]
    [Header("Stage1_Status")]
    public int health;
    

    /*
     * Player�� ����: 15
     * Player�� �ѹ� �������ϴµ� �ʿ��� ����ġ ���� 20
     * ���� ���ʹ� �Ѹ����� ����ġ 6 ��������1 ������ ����ġ 20
     */
    public int player_level;
    public int player_Exp;

    // ������ �ν��Ͻ��� ������ ���� ����
    private static SceneManagerEX instance;
    

    // �ٸ� ��ũ��Ʈ���� ������ �� ����� ������Ƽ
    public static SceneManagerEX Instance
    {
        get
        {
            // �ν��Ͻ��� ������ ����
            if (instance == null)
            {
                instance = FindObjectOfType<SceneManagerEX>();

                // Scene�� ������ ���� ����
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SceneManagerEX");
                    instance = singletonObject.AddComponent<SceneManagerEX>();
                }
            }

            // �̹� �����ϴ� ��� ���� �ν��Ͻ� ��ȯ
            return instance;
        }
    }

    // �̱����� ����� ������ �޼������ �Ʒ��� �߰�

    private void Awake()
    {
        // ������ �ν��Ͻ��� ���� �� �������� �ʵ��� ó��
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //restart�� �ʱ�ȭ
        player_level = 1;
        player_Exp = 0;
    }

    public void init()
    {
        player_level = 1;
        player_Exp = 0;
        totalTime = 0f;
        for(int i = 0; i < max_Status.Length; i++)
        {
            max_Status[i] = 5;
        }
    }
    //��������1 �̵� ��ũ��Ʈ
    public void LoadStage1()
    {
        beforeSceneName = "CharacterSelect";
        SceneManager.LoadScene("Loading");
    }

    //ĳ��â ��ũ��Ʈ
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

    public void LoadMainMenu()
    {
        init();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        //���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
