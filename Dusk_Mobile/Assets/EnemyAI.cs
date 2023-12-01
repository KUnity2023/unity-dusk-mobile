using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    CharacterStats mobstats;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    float attackDelay;
    public float dectectRange;
    public float atkRange;
    public int nextMove;
    public Transform currentTarget;
    // Start is called before the first frame update
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        mobstats = GetComponent<CharacterStats>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Patrol",5);

        dectectRange = 3.0f;
        atkRange = 1.0f;
    }  
    

    // Update is called once per frame
    void FixedUpdate()
    {
        attackDelay -= Time.deltaTime;
        if(attackDelay < 0) attackDelay = 0;
        float distance = Vector3.Distance(transform.position, currentTarget.position);
        if(attackDelay == 0 && distance <= dectectRange){
            FaceTarget();
            if(distance <= atkRange){
                AttackTarget();
            }else{
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    MoveToTarget();
                }
            }
        }
        else{
            //Move
            //rigid.velocity = new Vector2(nextMove,rigid.velocity.y);
            MoveToTarget();
        }
        
        //Check Platform
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f,rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down,new(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec,Vector3.down,1,LayerMask.GetMask("Platform"));
        if(rayHit.collider == null){
            Turn();
        }
    }
    void MoveToTarget()
    {
        float dir = currentTarget.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * Time.deltaTime);
        anim.SetInteger("Walkspeed", 1);
    }

    void FaceTarget()
    {
        if (currentTarget.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
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
        GameObject player = GameObject.Find("HeroKnight");
        player.GetComponent<CharacterStats>().TakeDamage(mobstats.damage.GetStat());
        anim.SetTrigger("attack"); // 공격 애니메이션 실행
        attackDelay = mobstats.atkSpd.GetStat(); // 딜레이 충전
    }
    void Patrol(){
        //Set Next move
        nextMove = Random.Range(-1,2);

        //Sprite Animation
        anim.SetInteger("Walkspeed", nextMove);

        //flip Sprite
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //Recursive
        float nextMoveTime = Random.Range(2f,5f);
        Invoke("Patrol",nextMoveTime);
    }

    void Turn(){
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Patrol",3);
    }

}
