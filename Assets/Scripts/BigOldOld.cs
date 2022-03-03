using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOldOld : CanBeRided
{
    public bool IsOnGround;

    private void Awake()
    {
    }
    
    void FixedUpdate()
    {
        IsOnGround = OnGround();
        
        if (OnRide)
        {
            WhenOnRide("Old");
            if (transform.GetChild(0).childCount != 0)
            {
                if (transform.GetChild(0).GetChild(0).name == "Big")
                {
                    GameObject.Find("Player").tag = "Big_Old";
                    gameObject.tag = "Big_Old";
                    transform.GetChild(0).tag = "Big_Old";
                }
            }
        }
        
        else if (!OnRide)
        {
            WhenNotOnRide("Old");
        }
    }
}
