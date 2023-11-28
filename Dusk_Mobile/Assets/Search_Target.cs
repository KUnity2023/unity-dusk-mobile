using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class Search_Target : StateMachineBehaviour
{
   private CharacterStats bossStat;
   public Transform player;
   Rigidbody2D rb;
   public float speed = 2.5f;
   public float cooldown = 8f;
   public float currentCooldown = 0f;
   public float attackRange = 3f;
   stage1boss boss;
   //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      //player = GameObject.FindGameObjectWithTag("Player").transform;
      
      player = GameObject.Find("HeroKnight").transform;
      rb = animator.GetComponent<Rigidbody2D>();
      bossStat = animator.GetComponent<CharacterStats>();
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
         animator.SetBool("isReady",true);
         animator.SetTrigger("Teleport");
         currentCooldown = cooldown;
      }
      
      Vector2 target = new Vector2(player.position.x, rb.position.y);
      //UnityEngine.Debug.Log(target);
      Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
      rb.MovePosition(newPos);

      if(Vector2.Distance(player.position, rb.position) <= attackRange && currentCooldown > 0){
         animator.SetTrigger("Attack");
      }
      //Move by teleport
      
   }

   //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      animator.ResetTrigger("Attack");
      animator.ResetTrigger("Teleport");
      animator.SetBool("isReady",false);
   }
}
