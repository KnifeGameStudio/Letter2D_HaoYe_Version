using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBook : MonoBehaviour
{
    [Header("与下落相关")]
    public Vector2 PointOffset;
    public Vector2 Size;
    public LayerMask GroundLayerMask;
    public Vector3 initialVelocity;
    public bool IsOnGround;
    public Vector3 velocity;

    [Header("与被骑过后消失相关")]
    public SpriteRenderer[] SPs;
    private Color originalColor;
    private Color changeColor;
    
    void Start()
    {
        initialVelocity = new Vector3(0,-10,0);
        velocity = initialVelocity;
        originalColor.a = 1f;
        changeColor.a = 0.1f;
        GroundLayerMask = LayerMask.GetMask("Ground");
    }
    
    void Update()
    {
        
        IsOnGround = OnGround();

        SPs = gameObject.GetComponentsInChildren<SpriteRenderer>();

        Falling();

    }

    void Falling()
    {
        if (transform.GetChild(0).CompareTag("Story") && !IsOnGround)
        {
            GetComponent<BoxCollider2D>().enabled = true;

            foreach (SpriteRenderer sp in SPs)
            {
                sp.color = originalColor;
            }
        }
        
        else if (transform.GetChild(0).CompareTag("Story") && IsOnGround)
        {
            velocity = Vector3.zero;
        }
        
        else if (!transform.GetChild(0).CompareTag("Story"))
        {
            foreach (SpriteRenderer sp in SPs)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                sp.color = changeColor;
                StartCoroutine(killSelf());
            }
        }
        
        transform.position += velocity * Time.deltaTime;
    }
    
    IEnumerator killSelf()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
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
