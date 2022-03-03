using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurryNewsShoot : MonoBehaviour
{
    public float speed;
    public float DestroyDistance;

    private Rigidbody2D rb2D;
    private Vector3 startPos;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * -speed;
        startPos = transform.position;
    }
    void FixedUpdate()
    {
        float Distance = (transform.position - startPos).sqrMagnitude;
        if (Distance > DestroyDistance)
        {
            Destroy(gameObject);
        }
    }
    
}
