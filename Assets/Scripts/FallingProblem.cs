using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FallingProblem : MonoBehaviour
{
    
    [Header("与下落相关")]
    public Vector2 PointOffset;
    public Vector2 Size;
    public LayerMask GroundLayerMask;
    //public Vector3 constantGravity = Vector3.down * 9.8f;
    public Vector3 initialVelocity;
    public bool IsOnGround;
    public Vector3 velocity;

    public Text my_Text;
    
    [Header("伤害")]
    public int damage;
    private PlayerHealth playerHealth;
    public GameObject player;
    void Start()
    {
        my_Text = GameObject.Find("Canvas/myText").GetComponent<UnityEngine.UI.Text>();
        velocity = initialVelocity;
        playerHealth= GameObject.Find("Player").GetComponent<PlayerHealth>();
        player = GameObject.Find("Player");
        GroundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        IsOnGround = OnGround();
        Falling();
    }

    void Falling()
    {
        if (IsOnGround)
        {
            velocity = Vector3.zero;
        }
        transform.position += velocity * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player"))
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            if (playerHealth != null && playerHealth.health > 3)
            {
                playerHealth.DamagePlayer(damage);
                my_Text.text = "这个题好难啊，得想个办法'做掉'这个题";
            }
            
            else if (playerHealth != null && playerHealth.health < 3)
            {
                playerHealth.DamagePlayer(0);
                my_Text.text = "快都要被题搞死了，得想个办法'做掉'这个题";
            }
        }
        
        else if (other.CompareTag("Player_Story"))
        {
            if (other.name == "Do")
            {
                SoundManager.playWriteClip();
                player.GetComponent<PlayerCTRL>().OffRideFromOther();
                Destroy(other.transform.parent.gameObject, 0.5f);
                Destroy(gameObject);
            }
        }
    }
    
    public bool OnGround()
    {
        Collider2D coll= Physics2D.OverlapBox((Vector2)transform.position + PointOffset,Size,0,GroundLayerMask);
        if (coll != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube((Vector2)transform.position + PointOffset,Size);
    }

}
