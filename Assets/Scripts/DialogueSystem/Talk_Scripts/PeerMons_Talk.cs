using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class PeerMons_Talk : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;
    public Text my_Text;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<PeerMons>().CanMove = false;
            Button.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerCTRL>().CanRide = false;
        }
        else if (other.gameObject.CompareTag("Player_Story"))
        {
            gameObject.GetComponent<PeerMons>().CanMove = false;
            my_Text.text = "做是什么意思？";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.CompareTag("Player_Story"))
        {
            gameObject.GetComponent<PeerMons>().CanMove = true;
            Button.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerCTRL>().CanRide = true;
            talkUI.SetActive(false);
        }
    }
    

    private void Update()
    {
        if (Button.activeSelf && Input.GetAxisRaw("Ride") == 1)
        {
            talkUI.SetActive(true);
        }
    }
}
