using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Door_DontWantGo : Door
{
    public Text my_Text;
    public GameObject player;
    public String words;
    void Start()
    {
        player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Invisible"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (Input.GetAxisRaw("Talk") == 1 && !playDoorSound)
            {
                playDoorSound = true;
                SoundManager.playLockedDoorClip();
                my_Text.text = words;                
                Debug.Log(words);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playDoorSound = false;
            //player.GetComponent<PlayerCTRL>().CanRide = true;
            my_Text.text = "";
        }
    }
}
