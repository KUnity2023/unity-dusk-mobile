using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideAtk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        
    }
    public void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision);
        if(collision.CompareTag("Player")){
            //Debug.Log("Attacked by spell");
            collision.GetComponent<CharacterStats>().TakeDamage(10);
            collision.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록
        }
    }
}
