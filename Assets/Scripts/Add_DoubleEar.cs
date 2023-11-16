using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Add_DoubleEar : MonoBehaviour
{
    public Text my_Text;
    public GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.GetComponent<PlayerCTRL>().CanRide == false)
        {
            my_Text.text = "It sounds like there's a noise, try pressing 'E.'";
            Debug.Log("It sounds like there's a noise, try pressing 'E.'");
            if (Input.GetAxisRaw("Ride") == 1)
            {
                other.tag = "Double_Ear";
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).position = new Vector2(other.transform.position.x+1.5f,other.transform.position.y);
                transform.GetChild(0).parent = other.transform.GetChild(0);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            my_Text.text = "";
        }
    }
}
