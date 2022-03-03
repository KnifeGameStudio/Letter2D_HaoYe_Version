using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour
{
    public Text my_Text;
    public GameObject player;
    public Vector3 initialVelocity;
    Vector3 velocity;
    public float moveTime;
    private float moveTimer;
    
    public bool DrawerIsOut;
    public bool DrawerCanMove;
    public bool CanInput = true;
    void Start()
    {
        player = GameObject.Find("Player");
        velocity = initialVelocity;
    }
    
    void Update()
    {
        MoveDrawer();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (!DrawerIsOut && CanInput)
            {
                my_Text.text = "按E操作抽屉";
                if (Input.GetAxis("Ride") == 1)
                {
                    SoundManager.playDrawerClip();
                    DrawerIsOut = true;
                    DrawerCanMove = true;
                    CanInput = false;
                }
            }
            
            else if (DrawerIsOut && CanInput)
            {
                my_Text.text = "按E操作抽屉";
                if (Input.GetAxis("Ride") == 1)
                {
                    SoundManager.playDrawerClip();
                    DrawerIsOut = false;
                    DrawerCanMove = true;
                    CanInput = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player.GetComponent<PlayerCTRL>().CanRide = true;
        my_Text.text = "";
    }

    void MoveDrawer()
    {
        if (!DrawerIsOut && DrawerCanMove)
        {
            if (moveTimer >= 0)
            {
                transform.position += velocity * Time.deltaTime;
                moveTimer -= Time.deltaTime;
            }
            else
            {
                DrawerCanMove = false;
                CanInput = true;
                moveTimer = moveTime;
            }
        }
        else if (DrawerIsOut && DrawerCanMove)
        {
            if (moveTimer >= 0)
            {
                transform.position -= velocity * Time.deltaTime;
                moveTimer -= Time.deltaTime;
            }
            else
            {
                DrawerCanMove = false;
                CanInput = true;
                moveTimer = moveTime;
            }
        }
    }

}
