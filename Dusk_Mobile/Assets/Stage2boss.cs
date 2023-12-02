using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2boss : MonoBehaviour
{
    private Animator ani;
    public Transform player;
    public LayerMask attackMask;
    public Transform melee;
    public int attackDamage = 10;
    public Vector2 boxSize;
    public GameObject spellPrefab;
    public GameObject spawnPrefab;
    
    private bool isFlipped = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxSize = new Vector2(2.3f,1.5f);
        attackMask = LayerMask.GetMask("Player");
        melee = GameObject.Find("BossMelee").transform;
        melee.localPosition = new Vector3(-0.05f,-0.3f,0f);
    }

    public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        
        if(transform.position.x > player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            isFlipped = false;
        }else if(transform.position.x < player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            isFlipped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //for debug
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(melee.position, boxSize);
    }
}
