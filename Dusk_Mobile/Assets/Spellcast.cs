using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcast : MonoBehaviour
{
    public CapsuleCollider2D CapsuleCol;
    public LayerMask attackMask;
    // Start is called before the first frame update
    private void Start() {
        CapsuleCol = GetComponent<CapsuleCollider2D>();
        attackMask = LayerMask.GetMask("Player");
    }
    
    public void removeSpell(){
        Destroy(gameObject,0.1f);
    }

    public void detectPlayer(){
        Collider2D[] colInfos = Physics2D.OverlapCapsuleAll(CapsuleCol.offset,CapsuleCol.size,CapsuleCol.direction,0f,attackMask);

        foreach(Collider2D colInfo in colInfos){
            Debug.Log(colInfo);
            if(colInfo.CompareTag("Player")){
            Debug.Log("Attacked by spell");
            colInfo.GetComponent<CharacterStats>().TakeDamage(20);
            colInfo.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
            }
        }
        
    }
}
