using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Manager : MonoBehaviour
{
    /*
     * 플레이어 현재 스탯 현황
     * 플레이어는 HP, Power, Attack Speed, Speed 4개의 Stat 소유
     * 예시로 사용하는 Hero Knight는 3 3 0 0으로 시작할 예정
     */
    
    private string targetTag = "Player"; // 찾고자 하는 태그
    private GameObject player;

    private int playerX = -8;
    private int playerY = -6;

    
    public GameObject sceneName;
    public GameObject[] stage;
    public GameObject stageMovePanel;
    public FadeEffect fade;
    private bool isBossDead;
    void Awake()
    {
        //Find Player
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        // 찾은 게임 오브젝트에 대한 처리
        foreach (GameObject obj in objectsWithTag)
        {
            // 찾은 게임 오브젝트에 대한 로직을 추가
            if (obj.tag == targetTag)
            {
                player = obj;
                break;
            }
        }
        //이러면 {2,2,5,5} 가 됨.
        for(int i = 0; i < 4; i++)
        {
            SceneManagerEX.Instance.max_Status[i] -= SceneManagerEX.Instance.HK_Start_Stat[i];
        }

        fade = stageMovePanel.GetComponent<FadeEffect>();
        isBossDead = true;
    }

    private void Update()
    {
        TurnScene();
        
    }
    void TurnScene()
    {
        if (sceneName.gameObject.name == "Stage1_1")
        {
            if (player.transform.position.x > 10)
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
            if(player.transform.position.y >= 3.5)
            {
                //Warp3_to_2
                Invoke("Warp3_to_2", 0.5f);
                fade.OnStageMove();
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
            if (isBossDead)
            {
                if (player.transform.position.x > 10)
                    SceneManagerEX.Instance.LoadStage2();
            }
        }
    }
    void warp_1_to_2()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerX, playerY);
        stage[0].SetActive(false);
        stage[1].SetActive(true);
        sceneName = stage[1];
    }

    void warp_2_to_1()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerX, playerY);
        stage[1].SetActive(false);
        stage[0].SetActive(true);
        sceneName = stage[0];
    }

    void Warp_2_to_4()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerX, playerY);
        stage[1].SetActive(false);
        stage[3].SetActive(true);
        sceneName = stage[3];
    }

    void Warp_2_to_3()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerX, playerY);
        stage[1].SetActive(false);
        stage[2].SetActive(true);
        sceneName = stage[2];
    }

    void Warp3_to_2()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerX, playerY);
        stage[2].SetActive(false);
        stage[1].SetActive(true);
        sceneName = stage[1];
    }

    void Warp4_to_2()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerX, playerY);
        stage[3].SetActive(false);
        stage[1].SetActive(true);
        sceneName = stage[1];
    }
    void Warp_4_to_6()
    {
        Debug.Log("player.transform.position.x: " + player.transform.position.x);
        player.transform.position = new Vector2(playerX, playerY);
        stage[3].SetActive(false);
        stage[4].SetActive(true);
        sceneName = stage[4];
    }
}
