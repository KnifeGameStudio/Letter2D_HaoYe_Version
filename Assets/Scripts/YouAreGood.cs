using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouAreGood : MonoBehaviour
{
    public float speed;
    public int heal;
    public float DestroyDistance;

    private Rigidbody2D rb2D;
    private Vector3 startPos;
    public PlayerHealth playerHealth;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * -speed;
        startPos = transform.position;
    }

    void Update()
    {
        playerHealth= GameObject.Find("Player").GetComponent<PlayerHealth>();
        float Distance = (transform.position - startPos).sqrMagnitude;
        if (Distance > DestroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("你真行啊！！！");
        if ((other.gameObject.CompareTag("Player"))
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            { 
                other.GetComponent<PlayerHealth>().HealPlayer(heal);
            }
        }
    }
}
