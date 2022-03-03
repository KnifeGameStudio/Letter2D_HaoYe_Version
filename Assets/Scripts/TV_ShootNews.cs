using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_ShootNews : MonoBehaviour
{

    public GameObject HurryNewsPrefab;
    public float shootWaitTime;
    private float shootWaitTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootHurryNews();
    }

    void shootHurryNews()
    {
        if (shootWaitTimer <= 0)
        {
            Instantiate(HurryNewsPrefab, transform.position, transform.rotation);
            shootWaitTimer = shootWaitTime;
        }
        
        else if (shootWaitTimer > 0)
        {
            shootWaitTimer -= Time.deltaTime;
        }
    }
}
