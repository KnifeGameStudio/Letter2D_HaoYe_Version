using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTalk : MonoBehaviour
{
    public GameObject talkUI;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talkUI.SetActive(false);
            other.GetComponent<PlayerCTRL>().CanRide = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCTRL>().CanRide = false;
            talkUI.SetActive(true);
        }
    }
}
