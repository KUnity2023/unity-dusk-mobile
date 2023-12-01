using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth { get; private set; }

    public Stats damage;
    public Stats atkSpd; 
    
    private void Awake() {
        curHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        Debug.Log(curHealth);
        GetComponent<Animator>().SetTrigger("Hurt");
        if(curHealth <= 0){
           StartCoroutine(Death());
        }
    }
    IEnumerator Death(){
        GetComponent<Animator>().SetTrigger("Death");
        if(gameObject.CompareTag("Enemy")){
            GetComponent<Enemy_AI_New>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<Collider2D>().enabled = false;
            /// give player EXP 
        }
        yield return new WaitForSeconds(6f);
        gameObject.SetActive(false);
        Destroy(gameObject,2);
        //사망시 추가 이벤트 자리
    }
}
