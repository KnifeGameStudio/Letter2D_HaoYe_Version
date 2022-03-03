using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("来自抽象类通用敌人选项")]
    //自己收到伤害值
    public int being_damaged;
    public int health;

    public Vector2 PointOffset;
    public Vector2 Size;
    public LayerMask GroundLayerMask;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
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
