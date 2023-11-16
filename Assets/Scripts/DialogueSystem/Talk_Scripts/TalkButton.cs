using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Invisible"))
        {
            Button.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerCTRL>().CanRide = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Invisible"))
        {
            Button.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerCTRL>().CanRide = true;
            talkUI.SetActive(false);
        }
    }
    

    private void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.UpArrow))
        {
            talkUI.SetActive(true);
        }
    }
}
