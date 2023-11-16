using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Peer : MonoBehaviour
{
    public Text my_Text;
    public GameObject player;
    public string[] wordlist;

    [Header("关于同学移动")] 
    public bool CanMove;
    
    public float LeftMoveTime;
    private float LeftMoveTimer;
    
    public float RightMoveTime;
    private float RightMoveTimer;
    public Rigidbody2D Rig;

    [Header("关于同学怪震动")] 
    public bool CanShake = false;
    public float shakeLeftTime;
    private float shakeLeftTimer;
    public float shakeRightTime;
    private float shakeRightTimer;
    
    
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        LeftRightMove();
    }
    
    void LeftRightMove()
    {
        if (CanMove && ! CanShake)
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
        else if (!CanMove && !CanShake)
        {
            Rig.velocity = Vector2.zero;
        }
        
        else if (!CanMove && CanShake)
        {
            if (shakeLeftTimer >= 0 && shakeRightTimer >= 0)
            {
                Rig.velocity = 1.5f * Vector2.left;
                shakeLeftTimer -= Time.deltaTime;
            }
            else if (shakeLeftTimer < 0 && shakeRightTimer >= 0)
            {
                Rig.velocity = 1.5f * Vector2.right;
                shakeRightTimer -= Time.deltaTime;
            }
            else if (shakeLeftTimer < 0 && shakeRightTimer < 0)
            {
                shakeLeftTimer= shakeLeftTime;
                shakeRightTimer = shakeRightTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.playPeerClip();
            player.GetComponent<PlayerCTRL>().CanRide = false;
            CanMove = false;
        }
        else if (other.CompareTag("Player_Old"))
        {
            SoundManager.playQustionClip();
            my_Text.text = "What is '佬'? It would be great if there were big shots(大佬).";
            CanMove = false;
        }
        else if (other.CompareTag("Big_Old"))
        {
            SoundManager.playScaredBreathClip();
            my_Text.text = "Wowww,It's a big shot(大佬), so impressive.";
            CanShake = true;
            CanMove = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            CanMove = false;
        }
        
        else if (other.CompareTag("Player_Old"))
        {
            my_Text.text = "What is '佬'? It would be great if there were big shots(大佬).";
            CanMove = false;
        }
        
        else if (other.CompareTag("Big_Old"))
        {
            my_Text.text = "Wowww,It's a big shot(大佬), so impressive.";
            CanShake = true;
            CanMove = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player_Old"))
        {
            my_Text.text = "";
            player.GetComponent<PlayerCTRL>().CanRide = true;
            CanMove = true;
            CanShake = false;
        }
        else if (other.CompareTag("Big_Old"))
        {
            my_Text.text = "";
            CanMove = true;
            CanShake = false;
        }
    }
}
