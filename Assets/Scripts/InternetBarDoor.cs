using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetBarDoor : Door
{
    public GameObject parents;
    public GameObject player;
    public GameObject outdoorback;
    public Text my_Text;

    public GameObject BGM_Control;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (Input.GetAxisRaw("Talk") == 1 && !playDoorSound)
            {
                BGM_Control.GetComponent<AudioSource>().Pause();
                playDoorSound = true;
                SoundManager.playLockedDoorClip();
                outdoorback.GetComponent<OutDoorBack>().WantInternet = true;
                my_Text.text = "Alright! I was wondering why no one was at home(屋), turns out you're out messing around at the internet cafe(网吧), have you gone mad?";
                player.GetComponent<PlayerCTRL>().CanInput = false;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(parentComingWait());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BGM_Control.GetComponent<AudioSource>().Play();
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
    }
}
