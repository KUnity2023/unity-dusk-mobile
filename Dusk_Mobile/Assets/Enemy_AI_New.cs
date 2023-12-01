using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_New : MonoBehaviour
{
    public Transform target;
    float attackDelay;
    public float dectectRange;
    public float atkRange;
    public float moveSpeed;
    public float atkSpeed;

    CharacterStats enemyStat;
    Animator enemyAnimator;
    Rigidbody2D rigid;
    void Start()
    {
        enemyStat = GetComponent<CharacterStats>();
        enemyAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if(attackDelay < 0) attackDelay = 0;
        if(target != null){
            float distance = Vector3.Distance(transform.position, target.position);

            if (attackDelay == 0 && distance <= dectectRange)
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
        Vector2 frontVec = new Vector2(rigid.position.x + 0.3f,rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down,new(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec,Vector3.down,1,LayerMask.GetMask("Platform"));
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
        target.GetComponent<CharacterStats>().TakeDamage(enemyStat.damage.GetStat());
        enemyAnimator.SetTrigger("Attack"); // 공격 애니메이션 실행
        attackDelay = atkSpeed; // 딜레이 충전
    }
}