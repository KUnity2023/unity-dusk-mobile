using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_New : MonoBehaviour
{
    public Transform target;
    private Transform melee;
    float attackDelay;
    public float dectectRange;
    public float atkRange;
    public float moveSpeed;
    public float atkSpeed;
    public Vector2 boxSize;

    CharacterStats enemyStat;
    Animator enemyAnimator;
    Rigidbody2D rigid;
    public LayerMask attackMask;
    void Start()
    {
        enemyStat = GetComponent<CharacterStats>();
        enemyAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        melee = transform.GetChild(0).transform;
        attackMask = LayerMask.GetMask("Player");
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if(attackDelay < 0) attackDelay = 0;
        if(target != null){
            float distance = Vector3.Distance(transform.position, target.position);
            float distanceY = Math.Abs(transform.position.y - target.position.y);

            if ((attackDelay == 0) && (distance <= dectectRange) && (distanceY <= 1.1f))
            {
                FaceTarget();

                if (distance <= atkRange)
                {
                    AttackTarget();
                }
                else
                {
                    if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        MoveToTarget();
                    }
                }
            }
            else
            {
                enemyAnimator.SetBool("moving", false);
            }
        }
    }

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        Vector2 frontVec = new Vector2(rigid.position.x + dir * 0.3f,rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down,new(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec,Vector3.down,1,LayerMask.GetMask("Platform","PassablePlatform"));
        if(rayHit.collider == null){
            enemyAnimator.SetBool("moving", false);
        }else{
            transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
            enemyAnimator.SetBool("moving", true);
        }
        
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
    }

    void AttackTarget()
    {
        enemyAnimator.SetTrigger("Attack"); // 공격 애니메이션 실행
        attackDelay = atkSpeed; // 딜레이 충전
        Collider2D colInfo = Physics2D.OverlapBox(melee.position,boxSize,0,attackMask);
        Debug.Log("colInfo: " + (colInfo != null));
        if (colInfo != null){
            if (!colInfo.GetComponent<CharacterManager>().blocking)
                colInfo.GetComponent<CharacterStats>().TakeDamage(enemyStat.damage.GetStat());
            colInfo.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(melee.position, boxSize);
    }
}