using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnManager : MonoBehaviour
{
    GameObject player;
    CharacterManager playerScript;
    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CharacterManager>();
    }
    public void JumpUp(){
        playerScript.inputJump = false;
    }
    public void JumpDown(){
        playerScript.inputJump = true;
    }
    
    public void AtkUp(){
        playerScript.inputAttack = false;
    } 
    public void AtkDown(){
        playerScript.inputAttack = true;
    } 

    public void RollUp(){
        playerScript.inputRoll = false;
    }
    public void RollDown(){
        playerScript.inputRoll = true;
    }
    public void GuardUp(){
        playerScript.inputGuard = false;
    }
    public void GuardDown(){
        playerScript.inputGuard = true;
    }
    
    
    public bool inputGuard;
    public bool inputRoll;
}
