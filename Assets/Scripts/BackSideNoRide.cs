using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackSideNoRide : MonoBehaviour
{
    // Start is called before the first frame update
    public Text my_Text;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            my_Text.text = "You can't see anything from this side. What do you want?";
        }
    }
}
