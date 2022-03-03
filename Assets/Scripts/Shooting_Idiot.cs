using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Idiot : MonoBehaviour
{
    public float speed;
    public int damage;
    public float DestroyDistance;

    private Rigidbody2D rb2D;
    private Vector3 startPos;
    private PlayerHealth playerHealth;

    public GameObject player;
    public GameObject teacherMons;
    
    void Start()
    {
        player = GameObject.Find("Player");
        teacherMons = GameObject.Find("TeacherMons");
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * -speed;
        startPos = transform.position;
        playerHealth= GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void FixedUpdate()
    {
        float Distance = (transform.position - startPos).sqrMagnitude;
        if (Distance > DestroyDistance)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("这是个傻X");
            if (playerHealth != null)
            {
                //this damage is the damage from enemy to the player
                playerHealth.DamagePlayer(damage);
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("这是个傻X");
            if (playerHealth != null)
            {
                //this damage is the damage from enemy to the player
                playerHealth.DamagePlayer(damage);
            }
        }
        
        /*else if (other.gameObject.CompareTag("Player_Stupid"))
        {
            Debug.Log("对面来者何人" + other.gameObject.name);
            if (other.gameObject.name == "Protect")
            {
                Debug.Log("他怎么变成保护了?");
                player.GetComponent<PlayerCTRL>().OffRideFromOther();
                //当对方为“保”时，攻击对方后立刻停止攻击
                teacherMons.GetComponent<TeacherMons>().Hitted = true;
                Destroy(other.transform.parent.gameObject, 0.5f);
                Destroy(gameObject);
            }
        }*/
    }
}
