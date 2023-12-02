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
            Debug.Log("pass platform", currentPassblePlatform);
            if(currentPassblePlatform != null){
                Debug.Log("passing floor");
                StartCoroutine(DisableCollision());
            }
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PassablePlatform")){
            currentPassblePlatform = collision.gameObject;
            Debug.Log("Enter col",currentPassblePlatform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PassablePlatform")){
            currentPassblePlatform = null;
            Debug.Log("Exit col",currentPassblePlatform);
        }
    }
    private IEnumerator DisableCollision(){
        TilemapCollider2D platformCollider = currentPassblePlatform.GetComponent<TilemapCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.75f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
