using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_To_BigOld : MonoBehaviour
{
    public BoxCollider2D Col;
    public Color changeColor;
    public Color currentColor;
    public SpriteRenderer SR;
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Col = GetComponent<BoxCollider2D>();
        currentColor = SR.color;
        changeColor = SR.color;
        changeColor.a = 255f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_Old"))
        {
            Debug.Log(other.name);
            gameObject.tag = "Big_Old";
            SR.color = changeColor;
            Col.size = new Vector2(Col.size.x * 3f, Col.size.y);
            transform.parent = other.transform;
            transform.position = new Vector2(other.transform.position.x - 2f, other.transform.position.y);
        }
    }
}
