using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelfRoom_DoorOut : Door
{
    public GameObject player;
    public Text my_Text;
    public GameObject parents;
    public GameObject homeworkMons;
    public GameObject BGM_Control;
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
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (Input.GetAxis("Talk") == 1 && !playDoorSound)
            {
                playDoorSound = true;
                if (homeworkMons != null)
                {
                    if (parents.GetComponent<Parent_Give_Problem>().problemExist)
                    {
                        BGM_Control.GetComponent<AudioSource>().Pause();
                        SoundManager.playLockedDoorClip();
                        my_Text.text = "Hurry up and do(做) the questions(题). Don't come out until you're done.";
                    }

                    else
                    {
                        BGM_Control.GetComponent<AudioSource>().Pause();
                        SoundManager.playLockedDoorClip();
                        my_Text.text = "You want to go out when you're done with your homework(作业)? Here's a new question(题). Take it and do(做) it!";
                        player.GetComponent<PlayerCTRL>().CanInput = false;
                        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        StartCoroutine(parentComingWait()); 
                    }
                }
                
                else if (homeworkMons == null)
                {
                    SoundManager.playDoorClip();
                    my_Text.text = "Homework(作业)'s done. Get out.";
                    PlayerPrefs.SetFloat("Home_SelfRoom_x", gameObject.transform.position.x);
                    PlayerPrefs.SetFloat("Home_SelfRoom_y", gameObject.transform.position.y);
                    PlayerPrefs.SetFloat("Home_SelfRoom_z", gameObject.transform.position.z);
                    player.GetComponent<PlayerCTRL>().CanInput = false;
                    StartCoroutine(GotoNext());
                }
            }
        }
    }
    
    IEnumerator GotoNext()
    {
        yield return new WaitForSeconds(1f);                
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (BGM_Control.activeSelf)
        {
            BGM_Control.GetComponent<AudioSource>().Play();
        }

        playDoorSound = false;
        player.GetComponent<PlayerCTRL>().CanRide = true;
        my_Text.text = "";
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
        parents.GetComponent<Parent_Give_Problem>().Now_Move = true;
    }
}
