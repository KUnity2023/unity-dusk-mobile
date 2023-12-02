using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCharMove : MonoBehaviour
{
    public GameObject tileMap;
    public GameObject ui;
    void Awake()
    {
        Invoke("UIActive", 3f);
    }

    void FixedUpdate()
    {
        Debug.Log(transform.position.x);
        RunChar();
    }

    void RunChar()
    {
        if(transform.position.x <= 0)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * 5, transform.position.y);
        }
    }
    void UIActive()
    {
        ui.SetActive(true);
    }
}
