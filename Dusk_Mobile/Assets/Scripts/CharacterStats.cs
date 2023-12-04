using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

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
            if (gameObject.CompareTag("Enemy"))
            {
                if(gameObject.name == "Stage1Boss")
                {
                    SceneManagerEX.Instance.player_Exp += 20;
                }
                else
                {
                    SceneManagerEX.Instance.player_Exp += 6;
                }
                
            }
            StartCoroutine(Death());
        }
    }
    IEnumerator Death(){
        GetComponent<Animator>().SetTrigger("Death");

        if (gameObject.CompareTag("Enemy")){
            if (gameObject.name != "Stage1Boss" && gameObject.name != "Stage2_Golem")
                GetComponent<Enemy_AI_New>().enabled = false;
            if (gameObject.name!= "Stage2_Golem")
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<Collider2D>().enabled = false;    
        }
        
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
        Destroy(gameObject,2);
        //사망시 추가 이벤트 자리
    }
}
