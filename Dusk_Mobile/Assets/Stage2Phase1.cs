using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Phase1 : StateMachineBehaviour
{
    public int attackDamage = 10;
    public int spellDamage = 20;

    public float cooldown = 2.5f;
    public float currentCooldown = 0f;
    private CharacterStats bossStat;
    private int numOfPatterns = 2;
    public Transform player;
    public bool isFlipped = false;
    Stage2boss boss;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldown = 2.2f;
        currentCooldown = 1.5f;
        numOfPatterns = 2;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossStat = animator.GetComponent<CharacterStats>();
        boss = animator.GetComponent<Stage2boss>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        if(currentCooldown <= 0)
        {
            animator.SetBool("patternOn",true);
            //animator.SetTrigger("pattern");
            animator.SetInteger("patternNum",Random.Range(1,numOfPatterns+1)); 
            currentCooldown = cooldown;
        }
        if(bossStat.curHealth<=500){
            animator.SetBool("Phase_2",true);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("patternOn",false);  
        animator.SetInteger("patternNum",0);  
    }
}
