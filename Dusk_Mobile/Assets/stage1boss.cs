using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    public int attackDamage = 10;
    public int spellDamage = 20;

    public Vector3 attackOffset;
    public Transform melee;
    public Vector2 boxSize;
    public float attackRange = 1f;
    public int mapSize = 20;
    public int mapoffset = -9;
    public LayerMask attackMask;
    public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        
        if(transform.position.x > player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }else if(transform.position.x < player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }
    }
    public void Attack(){
        Collider2D colInfo = Physics2D.OverlapBox(melee.position,boxSize,0,attackMask);
        Debug.Log(colInfo);
        if(colInfo != null){
            colInfo.GetComponent<CharacterStats>().TakeDamage(attackDamage);
        }
    }
    public void Teleport(){
        transform.position = new Vector3(Random.Range(-7,7),transform.position.y,transform.position.z);
        LookAtPlayer();
    }

    //for debug
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(melee.position, boxSize);
    }
}
