using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherMons_Talk : MonoBehaviour
{
    public GameObject talkUI;
    public GameObject player;
    public bool willTalk;
    public GameObject teacherMons;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
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
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (talkUI != null && willTalk)
            {
                teacherMons.GetComponent<TeacherMons>().enabled = false;
                teacherMons.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
