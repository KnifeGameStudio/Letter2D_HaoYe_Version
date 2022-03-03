using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chair_Rotate : MonoBehaviour
{
    public Text my_Text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            my_Text.text = "按E转动椅子";            
            if (Input.GetAxis("Ride") == 0)
            {
                transform.Rotate(new Vector3(0,1,0));
            }
        }
    }*/
}
