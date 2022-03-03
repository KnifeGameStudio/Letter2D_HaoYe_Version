using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ParentTalkButton : MonoBehaviour
{
    public GameObject talkUI;
    public GameObject player;
    public bool willTalk;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Invisible"))
        {
            if (talkUI != null)
            {
                talkUI.SetActive(false);
            }
            other.GetComponent<PlayerCTRL>().CanRide = true;
        }
    }
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Invisible"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (talkUI != null && willTalk)
            {
                talkUI.SetActive(true);
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player.GetComponent<PlayerCTRL>().CanInput = false;
                player.GetComponent<Animator>().SetFloat("Man_Walk", 0f);
            }
            
            else if (!willTalk)
            {
                player.GetComponent<PlayerCTRL>().CanInput = true;
            }
        }
    }
}
