using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warpManager : MonoBehaviour
{

    private int playerX = -8;
    private int playerY = -6;

    private string warp_name;
    private string[] warp_list = { "warp_11", "warp_12", "warp_13", "warp_14", "warp_15", "warp_16" };
    private string targetTag = "Player"; // ã���� �ϴ� �±�
    private GameObject player;

    public Stage1Manager stage1M;

    void Awake()
    {
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
        warp_name = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            for (int i = 0; i < warp_list.Length; i++)
            {
                if (warp_list[i] == warp_name)
                {
                    player.transform.position = new Vector2(playerX, playerY);
                    stage1M.sceneName.SetActive(false);
                    stage1M.sceneName.SetActive(true);
                }
            }
        }
    }
}
