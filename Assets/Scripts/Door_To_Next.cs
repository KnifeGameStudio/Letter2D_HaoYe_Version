using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Door_To_Next : Door
{
    [Header("门通往下一关+1或者上一关-1")]
    /*public int NextLevelIs;*/
    public String ThisDoorTo;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Invisible"))
        {
            other.GetComponent<PlayerCTRL>().CanRide = false;
            if (Input.GetAxisRaw("Ride") == 1 && !playDoorSound)
            {
                other.GetComponent<PlayerCTRL>().CanInput = false;
                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                playDoorSound = true;
                SoundManager.playDoorClip();
                StartCoroutine(GotoNext());
            }
        }
    }

    IEnumerator GotoNext()
    {
        yield return new WaitForSeconds(1f);                
        SceneManager.LoadScene(ThisDoorTo);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playDoorSound = false;
            other.GetComponent<PlayerCTRL>().CanRide = true;
        }
    }
}
