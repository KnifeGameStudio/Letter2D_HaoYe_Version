using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class CanBeRided : MonoBehaviour
{
    //此script为一抽象类，将会被所有的可以被骑的物体所通用
    public bool OnRide;
    private BoxCollider2D[] BoxColliders;
    
    public Vector2 PointOffset;
    public Vector2 Size;
    public LayerMask GroundLayerMask;
    public int N_of_Child;

    public GameObject RidingEffectPrefab;
    public bool PlayEffect;
    
    public void Start()
    {
        
    }

    public void Update()
    {

    }
    
    //当所有可以被骑的物体被骑的时候
    public void WhenOnRide(string selftag)
    {
        transform.position = transform.parent.position;
        gameObject.layer = 0;
        N_of_Child = transform.childCount;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).tag = "Player_" + selftag;
        
        transform.GetChild(1).gameObject.SetActive(false); 
        transform.GetChild(1).tag = "Player_" + selftag;

        gameObject.tag = "Player_" + selftag;
    }
    
    //当所有可以被骑的物体没有被骑的时候
    public void WhenNotOnRide(string selftag)
    {
        gameObject.layer = 8;
        N_of_Child = transform.childCount;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).tag = selftag;

        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).tag = selftag;

        gameObject.tag = selftag;
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
