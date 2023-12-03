using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    
    public GameObject player;
    public TextMeshProUGUI GuideText;
    public GameObject move;
    public GameObject jump;
    public GameObject attack;
    public GameObject roll;

    private string[] guideText= { 
    "Use JoyStick To Move",
    "Press Key to Jump",
    "PressDown JoyStick and Press Jump Key To Pass Floor",
    "Press Sword Key to Attack",
    "Now, Let's Escape this Castle!",
    "Press Button To Roll"
    };

    
    //4.6 18.6 39

    void FixedUpdate()
    {
        if(player.transform.position.x >= 47)
        {
            SceneManagerEX.Instance.LoadCharacterSelect();
        }

        if(player.transform.position.x>=-8.6 && player.transform.position.x <= 1)
        {
            DeActive();
            GuideText.text = guideText[0].ToString();
            move.SetActive(true);
        }
        if (player.transform.position.x >= 1 && player.transform.position.x <= 10)
        {
            DeActive();
            GuideText.text = guideText[5].ToString();
            roll.SetActive(true);
        }
        if (player.transform.position.x > 10 && player.transform.position.x <= 18.6)
        {
            DeActive();
            GuideText.text = guideText[1].ToString();
            jump.SetActive(true);
        }
        if(player.transform.position.x > 18.6 && player.transform.position.x <= 29)
        {
            DeActive();
            GuideText.text = guideText[2].ToString();
            jump.SetActive(true);
            move.SetActive(true);
        }
        if(player.transform.position.x > 29 && player.transform.position.x <= 39)
        {
            DeActive();
            GuideText.text = guideText[3].ToString();
            attack.SetActive(true);
        }
        if (player.transform.position.x > 39 && player.transform.position.x <= 47)
        {
            DeActive();
            GuideText.text = guideText[4].ToString();
        }

    }

    void DeActive()
    {
        move.SetActive(false);
        jump.SetActive(false);
        attack.SetActive(false);
        roll.SetActive(false);
    }

    
}
