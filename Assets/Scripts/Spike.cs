using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;
    private PlayerHealth playerHealth;
    public GameObject player;
    void Start()
    {
        player= GameObject.Find("Player");
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Single_Man") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            if (playerHealth != null)
            {
                //this damage is the damage from enemy to the player
                playerHealth.DamagePlayer(damage);
            }
        }

        else if (other.CompareTag("Player_Story"))
        {
            if (other.name == "Do")
            {
                player.GetComponent<PlayerCTRL>().OffRideFromOther();
                Destroy(other.transform.parent.gameObject, 0.5f);
            }
        }
    }
}
