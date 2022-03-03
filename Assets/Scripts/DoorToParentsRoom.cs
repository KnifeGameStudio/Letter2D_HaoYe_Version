using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DoorToParentsRoom : Door
{
    public GameObject BGM_Control;
    public Text my_Text;
    public GameObject parents;
    public GameObject player;

    public String DoorGoTo;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    void Update()
    {
        
    }
    IEnumerator GotoNext()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);                
        SceneManager.LoadScene(DoorGoTo);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Invisible"))
        {
            if (Input.GetAxisRaw("Ride") == 1 && !playDoorSound)
            {
                playDoorSound = true;
                SoundManager.playDoorClip();
                Debug.Log("FUCK!!!!");
                StartCoroutine(GotoNext());
            }
        }
        
        else if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (Input.GetAxisRaw("Ride") == 1 && !playDoorSound)
            {
                playDoorSound = true;
                SoundManager.playLockedDoorClip();
                BGM_Control.GetComponent<AudioSource>().Pause();
                my_Text.text = "你不准进来，大人的事小孩不要管";
                player.GetComponent<PlayerCTRL>().CanInput = false;
                player.GetComponent<Animator>().SetTrigger("No_Move");
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(parentComingWait());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playDoorSound = false;
            player.GetComponent<PlayerCTRL>().CanRide = true;
        }
    }

    IEnumerator parentComingWait()
    {
        yield return new WaitForSeconds(1.5f);
        parents.SetActive(true);
        /*player.GetComponent<PlayerCTRL>().CanInput = true;*/
        StartCoroutine(parentMoveWait());

    }

    IEnumerator parentMoveWait()
    {
        yield return new WaitForSeconds(2f);
        parents.GetComponent<Parents_Take_Back>().Now_Move = true;
        BGM_Control.GetComponent<AudioSource>().Play();
    }
}
