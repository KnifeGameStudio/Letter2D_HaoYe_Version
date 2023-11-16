using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TiredCurve : CanBeRided
{
    public bool IsOnGround;
    public Text my_Text;
    public bool playTired;
    private void Awake()
    {
        playTired = false;
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
            
            my_Text.text = "I'm too tired, please press 'F' to let me down.";
            if (!playTired)
            {
                SoundManager.playTiredClip();
                playTired = true;
            }
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            WhenOnRide("Curve");
        }

        else if (!OnRide)
        {
            PlayEffect = false;
            my_Text.text = "";
            WhenNotOnRide("Curve");
            playTired = false;
        }
    }
}
