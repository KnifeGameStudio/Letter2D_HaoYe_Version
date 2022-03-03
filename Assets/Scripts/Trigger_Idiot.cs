using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Idiot : MonoBehaviour
{
    public float speed;
    public int damage;
    public float DestroyDistance;

    private Rigidbody2D rb2D;
    private Vector3 startPos;
    public GameObject player;
    public GameObject teacherMons;
    private PlayerHealth playerHealth;

    public GameObject teacherMonsTalk;
    void Start()
    {
        teacherMonsTalk = GameObject.Find("TeacherMonsTalkTrigger");
        teacherMons = GameObject.Find("TeacherMons");
        player  = GameObject.Find("Player");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("这是个傻X");
            if (playerHealth != null)
            {
                //this damage is the damage from enemy to the player
                /*teacherMons.GetComponent<TeacherMons>().Hitted = true;*/
                playerHealth.DamagePlayer(damage);
            }
        }
        
        else if (other.gameObject.CompareTag("Player_Stupid"))
        {
            Debug.Log("对面来者何人" + other.gameObject.name);
            if (other.gameObject.name == "Protect")
            {
                teacherMonsTalk.transform.position = player.transform.position;
                teacherMonsTalk.GetComponent<TeacherMons_Talk>().willTalk = true;
                Debug.Log("他怎么变成保护了?");
                player.GetComponent<PlayerCTRL>().OffRideFromOther();
                //当对方为“保”时，攻击对方后立刻停止攻击
                teacherMons.GetComponent<TeacherMons>().Hitted = true;
                Destroy(other.transform.parent.gameObject, 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
