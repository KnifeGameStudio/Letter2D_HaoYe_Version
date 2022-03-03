using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRide_Talk : MonoBehaviour
{
    public GameObject talkUI;
    

    public void Update()
    {
        if (gameObject.GetComponent<CanBeRided>().OnRide)
        {
            talkUI.SetActive(true);
        }
        
        else if(!gameObject.GetComponent<CanBeRided>().OnRide)
        {
            talkUI.SetActive(false);
        }

    }
}
