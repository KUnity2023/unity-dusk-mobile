using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage2boss : MonoBehaviour
{
    private Animator ani;
    public Transform player;
    public LayerMask attackMask;
    public GameObject meleeRight;
    public GameObject meleeLeft;
    public int attackDamage = 10;
    public Vector2 boxSize;
    public GameObject spellPrefab;
    public GameObject spawnPrefab;
    
    public bool isFlipped = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxSize = new Vector2(2.3f,1.5f);
        attackMask = LayerMask.GetMask("Player");
        meleeRight = GameObject.Find("BossMelee_Right");
        meleeLeft = GameObject.Find("BossMelee_Left");
        //melee.localPosition = new Vector3(-0.05f,-0.3f,0f);
    }

    public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        Debug.Log("Look");
        if(transform.position.x > player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            isFlipped = true;
        }else if(transform.position.x < player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            isFlipped = false;
        }
    }
    public void phase1SideAtk(){
        //Anim, Golem_Attack_3
        LookAtPlayer();
        //Collider2D colInfo;
        boxSize = new Vector2(4f,4f);
        Vector2 melee;
        // if(isFlipped){ //True Left, False Right
        //     // melee.x = meleeLeft.position.x;
        //     // melee.y = meleeLeft.position.y;
        //     meleeLeft.GetComponent<BoxCollider2D>().enabled = true;
        // }else{
        //     // melee.x = meleeRight.position.x;
        //     // melee.y = meleeRight.position.y;
        //     meleeRight.GetComponent<BoxCollider2D>().enabled = true;
        // }
        melee.x = meleeRight.transform.position.x;
        melee.y = meleeRight.transform.position.y;
        Collider2D colInfo = Physics2D.OverlapBox(melee,boxSize,0,attackMask);
        
        Debug.Log("SideAtk"+colInfo+melee+attackMask);
        if(colInfo != null){
            if(!colInfo.GetComponent<CharacterManager>().blocking)
                colInfo.GetComponent<CharacterStats>().TakeDamage(attackDamage);
            colInfo.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
        }
    }
    public void phase1RangeAtk(){
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position,5.5f,attackMask);    
        if(colInfo != null){
            colInfo.GetComponent<CharacterStats>().TakeDamage(attackDamage);
            colInfo.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //for debug
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(meleeRight.transform.position, boxSize);
        //Gizmos.DrawWireCube(meleeLeft.transform.position, boxSize);
        Gizmos.DrawWireSphere(transform.position,5.5f);
    }

    public void turnOffCol(){
        meleeLeft.GetComponent<BoxCollider2D>().enabled = false;
        meleeRight.GetComponent<BoxCollider2D>().enabled = true;
    }
}
