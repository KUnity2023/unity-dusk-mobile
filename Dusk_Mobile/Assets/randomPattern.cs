using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPattern : StateMachineBehaviour
{
    public Transform player;
    Rigidbody2D rb;
    stage1boss boss;
    string newPattern;
    public float cooldown = 3f;
    public float currentCooldown = 0f;
    
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentCooldown = 0;
        player = GameObject.Find("HeroKnight").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<stage1boss>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        if(currentCooldown <= 0)
        {
            newPattern = boss.castRandomPattern();
            Debug.Log(newPattern);
            boss.LookAtPlayer();
            animator.SetTrigger(newPattern);
            currentCooldown = cooldown;
        }
        
        
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(newPattern);
        animator.SetBool("patternOn",false);
    }
}
