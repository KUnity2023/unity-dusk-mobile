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
}
