using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.transform.position.x >= 41)
        {
            SceneManagerEX.Instance.LoadStage1();
        }
    }
}
