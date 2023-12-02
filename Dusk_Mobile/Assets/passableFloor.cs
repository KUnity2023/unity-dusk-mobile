using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class passableFloor : MonoBehaviour
{
    private GameObject currentPassblePlatform;
    [SerializeField] private BoxCollider2D playerCollider;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            Debug.Log("pass platform");
            if(currentPassblePlatform != null){
                StartCoroutine(DisableCollision());
            }
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PassablePlatform")){
            Debug.Log(collision.gameObject.tag);
            currentPassblePlatform = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PassablePlatform")){
            Debug.Log(collision.gameObject.tag);
            currentPassblePlatform = null;
        }
    }
    private IEnumerator DisableCollision(){
        TilemapCollider2D platformCollider = currentPassblePlatform.GetComponent<TilemapCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.75f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
