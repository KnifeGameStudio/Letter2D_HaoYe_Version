using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProblem : MonoBehaviour
{
    public float speed;
    public int damage;
    public float DestroyDistance;

    private Rigidbody2D rb2D;
    private Vector3 startPos;
    private PlayerHealth playerHealth;
    public GameObject HWMonsTalk;
    public GameObject player;
    void Start()
    {

        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * -speed;
        startPos = transform.position;
        playerHealth= GameObject.Find("Player").GetComponent<PlayerHealth>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        HWMonsTalk = GameObject.Find("HWMonsTalkTrigger");
        float Distance = (transform.position - startPos).sqrMagnitude;
        if (Distance > DestroyDistance)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player"))
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
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
                SoundManager.playHWMonsComingClip();
                HWMonsTalk.GetComponent<HWMons_Talk>().willTalk = true;
                HWMonsTalk.transform.position = player.transform.position;
                SoundManager.playWriteClip();
                player.GetComponent<PlayerCTRL>().OffRideFromOther();
                Destroy(other.transform.parent.gameObject, 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
