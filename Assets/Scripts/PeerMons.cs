using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class PeerMons : MonoBehaviour
{
    public Animator anim;

    public bool CanMove;
    public float LeftMoveTime;
    private float LeftMoveTimer;
    
    public float RightMoveTime;
    private float RightMoveTimer;
    public Rigidbody2D Rig;

    public int damage;

    public Text my_Text;
    void Start()
    {
        anim = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LeftRightMove();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Curve"))
        {
            SoundManager.playDizzyClip();
            CanMove = false;
            anim.SetTrigger("PeerMonsDead");
            StartCoroutine(PeerMonsKillSelf());
        }
        
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(damage);
            Debug.Log("Weaklings, stay away from me!");
        }
        
        else if (other.gameObject.CompareTag("Player_Story"))
        {
            my_Text.text = "What does it mean by do(做)";
        }
    }

    IEnumerator PeerMonsKillSelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void LeftRightMove()
    {
        if (CanMove)
        {
            if (LeftMoveTimer >= 0 && RightMoveTimer >= 0)
            {
                Rig.velocity = 6f * Vector2.left;
                LeftMoveTimer -= Time.deltaTime;
            }
            else if (LeftMoveTimer < 0 && RightMoveTimer >= 0)
            {
                Rig.velocity = 6f * Vector2.right;
                RightMoveTimer -= Time.deltaTime;
            }
            else if (LeftMoveTimer < 0 && RightMoveTimer < 0)
            {
                LeftMoveTimer = LeftMoveTime;
                RightMoveTimer = RightMoveTime;
            }
        }
        else if (!CanMove)
        {
            Rig.velocity = Vector2.zero;
        }
    }
}
