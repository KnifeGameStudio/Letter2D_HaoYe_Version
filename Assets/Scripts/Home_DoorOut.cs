using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Home_DoorOut : Door
{
    public Text my_Text;
    public GameObject player;
    public string words;
    public string ThisDoorTo;
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
            if (Input.GetAxisRaw("Talk") == 1 && !playDoorSound)
            {
                if (PlayerPrefs.GetFloat("computer_Puzzle", 0f) == 1f)
                {
                    other.GetComponent<PlayerCTRL>().CanInput = false;
                    other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    playDoorSound = true;
                    SoundManager.playDoorClip();
                    Debug.Log("FUCK!!!!");
                    StartCoroutine(GotoNext());
                }
                else
                {
                    playDoorSound = true;
                    SoundManager.playLockedDoorClip();
                    my_Text.text = words;                
                    Debug.Log(words);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playDoorSound = false;
            player.GetComponent<PlayerCTRL>().CanRide = true;
            my_Text.text = "";
        }
    }
    
    IEnumerator GotoNext()
    {
        yield return new WaitForSeconds(1f);                
        SceneManager.LoadScene(ThisDoorTo);
    }
}
