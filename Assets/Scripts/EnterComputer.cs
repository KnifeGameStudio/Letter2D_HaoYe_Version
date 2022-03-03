using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnterComputer : Door
{
    [Header("门通往下一关+1或者上一关-1")]
    public Text my_Text;
    /*public int NextLevelIs;*/
    public string ThisDoorTo;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Invisible"))
        {
            other.GetComponent<PlayerCTRL>().CanRide = false;
            my_Text.text = "按E进入";
            if (Input.GetAxisRaw("Ride") == 1 && !playDoorSound)
            {
                other.GetComponent<PlayerCTRL>().CanInput = false;
                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                playDoorSound = true;
                SoundManager.playComputerWindowsClip();
                //此处应播进入电脑的声音
                Debug.Log("FUCK!!!!");
                PlayerPrefs.SetFloat("Home_BookRoom_x", gameObject.transform.position.x);
                PlayerPrefs.SetFloat("Home_BookRoom_y", gameObject.transform.position.y);
                PlayerPrefs.SetFloat("Home_BookRoom_z", gameObject.transform.position.z);
                StartCoroutine(GotoNext());
            }
        }
    }

    IEnumerator GotoNext()
    {
        
        yield return new WaitForSeconds(3f);                
        SceneManager.LoadScene(ThisDoorTo);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        playDoorSound = false;
        other.GetComponent<PlayerCTRL>().CanRide = true;
    }
}
