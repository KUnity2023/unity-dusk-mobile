using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        {7, 8},
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
        {4, 5},
        {10, 1}
    };
    public GameObject sceneName;
    public GameObject[] stage;
    public GameObject stageMovePanel;
    public FadeEffect fade;
    private bool isBossDead;

    void Awake()
    {
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
        for (int i = 0; i < 4; i++)
        {
            SceneManagerEX.Instance.max_Status[i] -= SceneManagerEX.Instance.HK_Start_Stat[i];
        }

        fade = stageMovePanel.GetComponent<FadeEffect>();
        //�����δ� Stage1_6���� ������ �����ؼ� �ǰ� 0���� �۾����� true�� ��ȯ �� ���� ������ �Ѿ�� �� ����
        isBossDead = true;
    }

    private void Update()
    {
        TurnScene();
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
        }
        else if(sceneName.gameObject.name == "Stage2_3")
        {

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
}
