using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcast : MonoBehaviour
{
    //public CapsuleCollider2D CapsuleCol;
    public BoxCollider2D boxCollider2D;
    public LayerMask attackMask;
    // Start is called before the first frame update
    private void Start() {
        //CapsuleCol = GetComponent<CapsuleCollider2D>();
        attackMask = LayerMask.GetMask("Player");
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    public void removeSpell(){
        Destroy(gameObject,0.1f);
    }

    public void detectPlayer(){
        //Collider2D[] colInfos = Physics2D.OverlapCapsuleAll(CapsuleCol.offset,CapsuleCol.size,CapsuleCol.direction,0f,attackMask);
        Collider2D colInfo = Physics2D.OverlapBox(boxCollider2D.offset, boxCollider2D.size,0,attackMask);

        //foreach(Collider2D colInfo in colInfos){
        Debug.Log(colInfo);

        if(colInfo.CompareTag("Player")){
            Debug.Log("Attacked by spell");
            colInfo.GetComponent<CharacterStats>().TakeDamage(20);
            colInfo.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
        }
        //}
        
    }
    private void OnTriggerEnter2D(Collision2D collision){
        Debug.Log(collision.gameObject);
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("Attacked by spell");
            collision.gameObject.GetComponent<CharacterStats>().TakeDamage(20);
            collision.gameObject.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
        }
    }
    
}
