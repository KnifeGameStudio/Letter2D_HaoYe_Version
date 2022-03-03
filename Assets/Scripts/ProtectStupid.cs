using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectStupid : CanBeRided
{
    [Header("不骑过后消失相关")]
    /*public SpriteRenderer[] SPs;*/
    /*private Color originalColor;
    private Color changeColor;

    public bool RidedOnce;*/
    public bool IsOnGround;
    private void Awake()
    {
        /*originalColor.a = 1f;
        changeColor.a = 0.1f;*/
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
            WhenOnRide("Stupid");
            //RidedOnce = true;
        }
        
        else if (!OnRide)
        {
            PlayEffect = false;
            WhenNotOnRide("Stupid");
        }
        
        /*SPs = gameObject.GetComponentsInChildren<SpriteRenderer>();*/
        //CheckBeingRided();
    }
    
    
    /*void CheckBeingRided()
    {
        if (!RidedOnce)
        {
            foreach (SpriteRenderer sp in SPs)
            {
                sp.color = originalColor;
            }
        }
            
        else if(RidedOnce)
        {
            foreach (SpriteRenderer sp in SPs) 
            {
                sp.color = changeColor;
                StartCoroutine(killSelf());
            }
        }
    }*/
    
    /*IEnumerator killSelf()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }*/
}
