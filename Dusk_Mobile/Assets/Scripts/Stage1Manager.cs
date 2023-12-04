using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Manager : MonoBehaviour
{
    /*
     * �÷��̾� ���� ���� ��Ȳ
     * �÷��̾�� HP, Power, Attack Speed, Speed 4���� Stat ����
     * ���÷� ����ϴ� Hero Knight�� 3 3 0 0���� ������ ����
     */
    
    private string targetTag = "Player"; // ã���� �ϴ� �±�
    private GameObject player;

    private float playerX = -8;
    private float playerY = -5;

    //Y�� ���� �̽��� SelectedChar�� ���� ������
    private float hero3rdOffset = -0.8f;
    private float herokingOffset = -1.6f;


    public Transform playerSpawn;
    public GameObject sceneName;
    public GameObject[] stage;
    public GameObject stageMovePanel;
    public FadeEffect fade;
    public GameObject Stage1_Boss;
    public GameObject stage2Entry;

    public GameObject[] hiddenEnemy;
    [Header("Characters")]
    public GameObject[] playerChar;

    private bool isBossDead;
    private CharacterStats bossStatus;
    private bool isBoxOpen =false;

    [Header("BOSS")]
    public TextMeshProUGUI BOSSHPText;
    public GameObject BOSShealthBar;
    private Slider BhealthBar;
    public GameObject stage2Door;
    void Awake()
    {
        Instantiate(SceneManagerEX.Instance.selectChar);
        //Find Player
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        // ã�� ���� ������Ʈ�� ���� ó��
        foreach (GameObject obj in objectsWithTag)
        {
            // ã�� ���� ������Ʈ�� ���� ������ �߰�
            if (obj.tag == targetTag)
            {
                player = obj;
                break;
            }
        }
        //�̷��� {2,2,5,5} �� ��.
        for(int i = 0; i < 4; i++)
        {
            if(SceneManagerEX.Instance.selectChar.name == "HeroKnight")
            {
                SceneManagerEX.Instance.max_Status[i] -= SceneManagerEX.Instance.HK_Start_Stat[i];
            }
            else if(SceneManagerEX.Instance.selectChar.name == "HeroKing")
            {
                SceneManagerEX.Instance.max_Status[i] -= SceneManagerEX.Instance.King_Start_Stat[i];
            }
            else if(SceneManagerEX.Instance.selectChar.name == "Hero3rd")
            {
                SceneManagerEX.Instance.max_Status[i] -= SceneManagerEX.Instance.Women_Start_Stat[i];
            }
            
        }

        fade = stageMovePanel.GetComponent<FadeEffect>();
        isBossDead = false;
        BhealthBar = BOSShealthBar.GetComponent<Slider>();
    }
    void Start()
    {
        bossStatus = Stage1_Boss.GetComponent<CharacterStats>();
    }
    private void Update()
    {
        TurnScene();
        BOSShealth();
    }

    void BOSShealth()
    {
        if(sceneName.gameObject.name == "Stage1_6" && !isBossDead)
        {
            BOSShealthBar.SetActive(true);
            //�÷��̾��� ü���� ���� (�÷��̾� ü���� �Դٰ��� �ϴ� ���� ��ġ)
            BhealthBar.value = ((float)Stage1_Boss.GetComponent<CharacterStats>().curHealth / (float)Stage1_Boss.GetComponent<CharacterStats>().maxHealth);
            //Debug.Log("PlayerHealthValue"+healthBar.value);
            //Debug.Log("PlayerHealth: " + characterStats.curHealth + "/" + characterStats.maxHealth);
            BOSSHPText.text = Stage1_Boss.GetComponent<CharacterStats>().curHealth.ToString() + "/" + Stage1_Boss.GetComponent<CharacterStats>().maxHealth.ToString();
        }
    }
    void TurnScene()
    {
        if (sceneName.gameObject.name == "Stage1_1")
        {
            if (player.transform.position.x >9.5)
            {
                //warp_1_to_2
                Invoke("warp_1_to_2", 0.5f);
                fade.OnStageMove();
            }
        }
        else if (sceneName.gameObject.name == "Stage1_2") 
        {
            if(player.transform.position.x < -10.5)
            {
                //warp_2_to_1
                Invoke("warp_2_to_1", 0.5f);
                fade.OnStageMove();
            }
            else if(player.transform.position.x > 10.5)
            {
                //Warp_2_to_4
                Invoke("Warp_2_to_4", 0.5f);
                fade.OnStageMove();
            }
            else if(player.transform.position.y < -7)
            {
                //Warp_2_to_3
                Invoke("Warp_2_to_3", 0.5f);
                fade.OnStageMove();
            }
        }
        else if(sceneName.gameObject.name == "Stage1_3")
        {
            if(player.transform.position.y >= 3.5 && player.transform.position.x >= 0)
            {
                //Warp3_to_2
                Invoke("Warp3_to_2", 0.5f);
                fade.OnStageMove();
            }

            if(player.transform.position.y <= -4.2 && (player.transform.position.x >= -0.66 && player.transform.position.x <= 0.74))
            {
                for (int idx = 0; idx < 10; idx++)
                {
                    hiddenEnemy[idx].SetActive(true);
                }
            }
        }
        else if(sceneName.gameObject.name == "Stage1_4")
        {
            if (player.transform.position.x < -10.5)
            {
                //Warp4_to_2
                Invoke("Warp4_to_2", 0.5f);
                fade.OnStageMove();

            }
            else if (player.transform.position.x < -8.5 && player.transform.position.y > 0.9)
            {
                //Warp_4_to_6
                Invoke("Warp_4_to_6", 0.5f);
                fade.OnStageMove();
            }
        }
        else
        {
            //Stage1_6(Boss Room)
            //���� �Ƿ� isBossDead ���� �Ǻ�
            if(bossStatus.curHealth <= 0)
            {
                isBossDead = true;
                
            }
            if (isBossDead)
            {
                stage2Door.SetActive(true);
                BOSShealthBar.SetActive(false);
                if (player.transform.position.x > 10)
                {
                    
                    SceneManagerEX.Instance.health = player.GetComponent<CharacterStats>().curHealth;
                    SceneManagerEX.Instance.LoadStage2();
                }
                    
            }
        }
    }
    void warp_1_to_2()
    {
        if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
        {
            player.transform.position = new Vector2(playerX, playerY - hero3rdOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
        {
            player.transform.position = new Vector2(playerX, playerY - herokingOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
        {
            player.transform.position = new Vector2(playerX, playerY);
        }
        stage[0].SetActive(false);
        stage[1].SetActive(true);
        sceneName = stage[1];
    }

    void warp_2_to_1()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        if(SceneManagerEX.Instance.selectChar.name == "Hero3rd")
        {
            player.transform.position = new Vector2(playerX, playerY - hero3rdOffset);
        }
        else if(SceneManagerEX.Instance.selectChar.name == "HeroKing")
        {
            player.transform.position = new Vector2(playerX, playerY- herokingOffset);
        }
        else if(SceneManagerEX.Instance.selectChar.name == "HeroKnight")
        {
            player.transform.position = new Vector2(playerX, playerY);
        }
        stage[1].SetActive(false);
        stage[0].SetActive(true);
        sceneName = stage[0];
    }

    void Warp_2_to_4()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
        {
            player.transform.position = new Vector2(playerX, playerY - hero3rdOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
        {
            player.transform.position = new Vector2(playerX, playerY - herokingOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
        {
            player.transform.position = new Vector2(playerX, playerY);
        }
        stage[1].SetActive(false);
        stage[3].SetActive(true);
        sceneName = stage[3];
    }

    void Warp_2_to_3()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
        {
            player.transform.position = new Vector2(playerX, playerY - hero3rdOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
        {
            player.transform.position = new Vector2(playerX, playerY - herokingOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
        {
            player.transform.position = new Vector2(playerX, playerY);
        }
        stage[1].SetActive(false);
        stage[2].SetActive(true);
        sceneName = stage[2];
    }

    void Warp3_to_2()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
        {
            player.transform.position = new Vector2(playerX, playerY - hero3rdOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
        {
            player.transform.position = new Vector2(playerX, playerY - herokingOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
        {
            player.transform.position = new Vector2(playerX, playerY);
        }
        stage[2].SetActive(false);
        stage[1].SetActive(true);
        sceneName = stage[1];
    }

    void Warp4_to_2()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
        {
            player.transform.position = new Vector2(playerX, playerY - hero3rdOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
        {
            player.transform.position = new Vector2(playerX, playerY - herokingOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
        {
            player.transform.position = new Vector2(playerX, playerY);
        }
        stage[3].SetActive(false);
        stage[1].SetActive(true);
        sceneName = stage[1];
    }
    void Warp_4_to_6()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
        {
            player.transform.position = new Vector2(playerX, playerY - hero3rdOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
        {
            player.transform.position = new Vector2(playerX, playerY - herokingOffset);
        }
        else if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
        {
            player.transform.position = new Vector2(playerX, playerY);
        }
        stage[3].SetActive(false);
        stage[4].SetActive(true);
        sceneName = stage[4];
    }
}
