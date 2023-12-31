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
    public GameObject spellPrefab;
    public float attackRange = 1f;
    public int mapSize = 20;
    public int mapoffset = -9;
    
    public LayerMask attackMask;
    private Animator ani;
    public void Start() {
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxSize = new Vector2(2.3f,1.5f);
        melee.localPosition = new Vector3(-0.05f,-0.3f,0f);
        attackMask = LayerMask.GetMask("Player");
    }
    public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        
        if(transform.position.x > player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            transform.position = new Vector3(transform.position.x - 3f,transform.position.y,transform.position.z);
            isFlipped = false;
        }else if(transform.position.x < player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            transform.position = new Vector3(transform.position.x + 3f,transform.position.y,transform.position.z);
            isFlipped = true;
        }
    }
    public void Attack(){
        boxSize = new Vector2(2.3f,1.5f);
        melee.localPosition = new Vector3(-0.05f,-0.3f,0f);
        Collider2D colInfo = Physics2D.OverlapBox(melee.position,boxSize,0,attackMask);
        if(colInfo != null){
            if(!colInfo.GetComponent<CharacterManager>().blocking)
                colInfo.GetComponent<CharacterStats>().TakeDamage(attackDamage);
            colInfo.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록

        }
    }
    public void Teleport(){
        transform.position = new Vector3(Random.Range(-7,7),transform.position.y,transform.position.z);
        LookAtPlayer();
        ani.SetFloat("distance",Vector2.Distance(transform.position,player.position));
    }
    public void RangeAttack(){
        boxSize = new Vector2(5,4);
        melee.localPosition = new Vector3(-0.15f,0,0);
        Collider2D colInfo = Physics2D.OverlapBox(melee.position,boxSize,0,attackMask);
        if(colInfo != null){
            if(!colInfo.GetComponent<CharacterManager>().blocking)
                colInfo.GetComponent<CharacterStats>().TakeDamage(attackDamage);
            colInfo.GetComponent<CharacterManager>().OnDamaged(transform.position);
            //맞은 대상의 레이어를 잠시 바꾸고 일정시간 데미지가 안들어가도록

        }
        boxSize = new Vector2(2.3f,1.5f);
        melee.localPosition = new Vector3(-0.05f,-0.3f,0f);
    }
    public void CastSpell(){
        int num = Random.Range(2,5);
        for(int i = 0; i <num;i++){
            Vector3 newPos = new Vector3(Random.Range(-6,6),-1.23f,0);
            Instantiate(spellPrefab,newPos,transform.rotation);
        }
        
    }
    enum patterns{
        RangeAttack,
        Cast
        //CastNoEff
    }
    public string castRandomPattern(){
        var enumValues = System.Enum.GetValues(enumType:typeof(patterns));
        string pattern = enumValues.GetValue(Random.Range(0,enumValues.Length)).ToString();
        return pattern;
    }

    //for debug
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(melee.position, boxSize);
    }
}
