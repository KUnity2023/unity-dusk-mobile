using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2Manager : MonoBehaviour
{
    private string targetTag = "Player"; // ã���� �ϴ� �±�

    private GameObject player;
    /*
     *Stage2_(index+1) PlayerSpawn Position
     *Stage1_Boss->Stage2_1
     *Stage2_1->Stage2_2
     *Stage2_2->Stage2_1
     *Stage2_2->Stage2_3
     */
    private int[,] playerSpawnXY = new int[,] {
        {-9, -6},
        {0, -6},
        {0, 2},
        {-9, -6},
    };
    /*�÷��̾ ���� ������������ �̵��� ���� ����
     *
     *Stage2_1->Stage2_2
     *Stage2_2->Stage2_1
     *Stage2_2->Stage2_3
     */
    private int[,] playerWarpXY = new int[,] {
        {-1, 4},
        {-10, -5},
        {10, 1}
    };
    public GameObject sceneName;
    public GameObject[] stage;
    public GameObject stageMovePanel;
    public GameObject stage2_Boss;
    public FadeEffect fade;

    private bool isBossDead;
    private CharacterStats bossStatus;

    [Header("BOSS")]
    public TextMeshProUGUI BOSSHPText;
    public GameObject BOSShealthBar;
    private Slider BhealthBar;
    void Awake()
    {
        Instantiate(SceneManagerEX.Instance.selectChar);
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

        player.GetComponent<CharacterStats>().maxHealth = SceneManagerEX.Instance.max_Status[0] * 50 + 50;

        fade = stageMovePanel.GetComponent<FadeEffect>();
        //�����δ� Stage1_6���� ������ �����ؼ� �ǰ� 0���� �۾����� true�� ��ȯ �� ���� ������ �Ѿ�� �� ����
        isBossDead = false;
        bossStatus = stage2_Boss.GetComponent<CharacterStats>();
        BhealthBar = BOSShealthBar.GetComponent<Slider>();
    }

    private void Update()
    {
        TurnScene();
        BOSShealth();
    }
    void BOSShealth()
    {
        if (sceneName.gameObject.name == "Stage2_3")
        {
            BOSShealthBar.SetActive(true);
            //�÷��̾��� ü���� ���� (�÷��̾� ü���� �Դٰ��� �ϴ� ���� ��ġ)
            BhealthBar.value = ((float)stage2_Boss.GetComponent<CharacterStats>().curHealth / (float)stage2_Boss.GetComponent<CharacterStats>().maxHealth);
            //Debug.Log("PlayerHealthValue"+healthBar.value);
            //Debug.Log("PlayerHealth: " + characterStats.curHealth + "/" + characterStats.maxHealth);
            BOSSHPText.text = stage2_Boss.GetComponent<CharacterStats>().curHealth.ToString() + "/" + stage2_Boss.GetComponent<CharacterStats>().maxHealth.ToString();
        }
    }
    void TurnScene()
    {
        if(sceneName.gameObject.name == "Stage2_1")
        {
            if(player.transform.position.x >= playerWarpXY[0,0] && player.transform.position.y >= playerWarpXY[0, 1])
            {
                Invoke("warp_1_to_2", 0.5f);
                fade.OnStageMove();
            }
        }
        else if(sceneName.gameObject.name == "Stage2_2")
        {
            if (player.transform.position.x >= playerWarpXY[2, 0] && player.transform.position.y >= playerWarpXY[2, 1])
            {
                Invoke("warp_2_to_3", 0.5f);
                fade.OnStageMove();
            }
            if(player.transform.position.x <= playerWarpXY[1, 0] && player.transform.position.y <= playerWarpXY[1, 1])
            {
                Invoke("warp_2_to_1", 0.5f);
                fade.OnStageMove();
            }
        }
        else if(sceneName.gameObject.name == "Stage2_3")
        {
            if(bossStatus.curHealth<= 0)
            {
                isBossDead = true;

                if (isBossDead)
                {
                    Invoke("LoadClearScene", 6);
                }
            }
        }
    }

    void warp_1_to_2()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerSpawnXY[1,0], playerSpawnXY[1,1]);
        stage[0].SetActive(false);
        stage[1].SetActive(true);
        sceneName = stage[1];
    }

    void warp_2_to_3()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerSpawnXY[3, 0], playerSpawnXY[3, 1]);
        stage[1].SetActive(false);
        stage[2].SetActive(true);
        sceneName = stage[2];
    }

    void warp_2_to_1()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerSpawnXY[2, 0], playerSpawnXY[2, 1]);
        stage[1].SetActive(false);
        stage[0].SetActive(true);
        sceneName = stage[0];
    }

    void LoadClearScene()
    {
        SceneManager.LoadScene("GameClear");
    }
}
