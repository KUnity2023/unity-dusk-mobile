using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcast : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start() {
        
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<CharacterStats>().TakeDamage(20);
            other.GetComponent<CharacterManager>().OnDamaged(transform.position);
        }
        //Destroy(gameObject,2.5f);
    }
    public void removeSpell(){
        Destroy(gameObject,0.1f);
    }
}
