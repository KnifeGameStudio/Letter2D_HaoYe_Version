using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoStory : CanBeRided
{

    public bool IsOnGround;
    

    private void Awake()
    {
        GroundLayerMask = LayerMask.GetMask("Ground");
    }
    
    void FixedUpdate()
    {
        IsOnGround = OnGround();

        if (OnRide)
        {
            if (!PlayEffect)
            {
                PlayEffect = true;
                Instantiate(RidingEffectPrefab, transform.position,transform.rotation);
            }
            WhenOnRide("Story");
        }
        
        else if (!OnRide)
        {
            PlayEffect = false;
            WhenNotOnRide("Story");
        }
        
    }
    
}
