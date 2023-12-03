using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class Spellcast : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public LayerMask attackMask;
    // Start is called before the first frame update
    private void Start() {
        attackMask = LayerMask.GetMask("Player");
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    public void removeSpell(){
        Destroy(gameObject,0.1f);
    }

    public void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision);
        if(collision.CompareTag("Player")){
            Debug.Log("Attacked by spell");
            collision.GetComponent<CharacterStats>().TakeDamage(20);
            collision.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
        }
    }
    
}
