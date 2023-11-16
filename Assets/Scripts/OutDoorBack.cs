using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutDoorBack : Door
{
    public bool WantInternet = false;
    public Text my_Text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !WantInternet)
        {
            /*my_Text.text = "按E进入";*/
            other.GetComponent<PlayerCTRL>().CanRide = false;
            if (Input.GetAxisRaw("Ride") == 1 && !playDoorSound)
            {
                playDoorSound = true;
                SoundManager.playLockedDoorClip();
                my_Text.text = "好不容易才出来的，还是不要回去了吧";
                Debug.Log("好不容易才出来的，还是不要回去了吧");
            }
        }

        else if (other.CompareTag("Player") && WantInternet)
        {
            Debug.Log("!!!!");
            SceneManager.LoadScene("Ch1Ending");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            playDoorSound = false;
            my_Text.text = "";
            other.GetComponent<PlayerCTRL>().CanRide = true;
        }
    }
}

