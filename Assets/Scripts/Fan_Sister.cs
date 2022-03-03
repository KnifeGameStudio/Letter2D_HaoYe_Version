using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Fan_Sister : MonoBehaviour
{
    
    public GameObject player;

    [Header("关于迷妹移动")] 
    public bool CanMove;

    public float RightMoveTime;
    private float RightMoveTimer;
    public Rigidbody2D Rig;

    public GameObject rightBond;
    
    void Start()
    {
        RightMoveTimer = RightMoveTime;
        Rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }
    
    void Update()
    {
        Invoke("LeftRightMove", 5f);
    }
    
    void LeftRightMove()
    {
        if (transform.position.x < rightBond.transform.position.x -2f)
        {
            if (CanMove)
            {
                if (RightMoveTimer >= 0)
                {
                    Rig.velocity = 3f * Vector2.right;
                    RightMoveTimer -= Time.deltaTime;
                }
            }
            else if (!CanMove)
            {
                Rig.velocity = Vector2.zero;
            }
        }
        else if (transform.position.x >= rightBond.transform.position.x - 2f)
        {
            Rig.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.playGirlClip();
            player.GetComponent<PlayerCTRL>().CanRide = false;
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

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = true;
            CanMove = true;
        }
    }
}
