using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    public string beforeSceneName;
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

    // �̱����� ����� ������ �޼������ ���⿡ �߰�

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

    public void LoadStage1()
    {
        beforeSceneName = "TutorialMap";
        SceneManager.LoadScene("Loading");
    }

    public void LoadTutorial()
    {
        beforeSceneName = "MainMenu";
        SceneManager.LoadScene("Loading");
    }
}
