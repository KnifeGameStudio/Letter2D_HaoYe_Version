using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestTree : CanBeRided
{

    public GameObject player;
    public float recover_Time;
    private float timer;
    public int player_health;
    public int player_maxi_health;
    public int heal;
    public bool is_Healing = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        player_maxi_health = player.GetComponent<PlayerHealth>().maxi_helath;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (OnRide)
        {
            if (!PlayEffect)
            {   
                PlayEffect = true;
                Instantiate(RidingEffectPrefab, transform.position,transform.rotation);
            }
            
            player_health = player.GetComponent<PlayerHealth>().health;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<PlayerCTRL>().CanMove = false;
            player.GetComponent<PlayerCTRL>().CanJump = false;
            WhenOnRide("Tree");
            Heal_It();
        }
        
        else if (!OnRide)
        {
            PlayEffect = false;
            WhenNotOnRide("Tree");
        }
    }

    void Heal_It()
    {
        timer -= Time.fixedDeltaTime;
        if (timer > 0 && is_Healing)
        {
            if (player_health < player_maxi_health)
            {
                player.GetComponent<PlayerHealth>().HealPlayer(heal);
                is_Healing = false;
            }
        }
        else if (timer <= 0)
        {
            timer = recover_Time;
            is_Healing = true;
        }
    }
}
