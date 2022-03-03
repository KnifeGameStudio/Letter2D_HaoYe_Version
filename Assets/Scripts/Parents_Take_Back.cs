using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parents_Take_Back : MonoBehaviour
{
    public bool Now_Move = false;
    public float moveTime;
    private float moveTimer;
    public GameObject player;
    public Vector3 initialVelocity;
    Vector3 velocity;
    public Vector3 originalPos;
    void Start()
    {
        player = GameObject.Find("Player");
        moveTimer = moveTime;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Now_Move)
        {
            /*Debug.Log("我要动了！！！");*/
            takePlayerBack();
        }
    }
    
    void takePlayerBack()
    {
        if (moveTimer >= 0)
        {
            velocity = initialVelocity;
            transform.position -= velocity * Time.deltaTime;
            moveTimer -= Time.deltaTime;
        }

        else
        {
            StartCoroutine(GobackWait());
        }
    }
    
    
    IEnumerator GobackWait()
    {
        velocity = Vector3.zero;
        player.GetComponent<PlayerCTRL>().CanInput = true;
        yield return new WaitForSeconds(2f);
        Recover();
    }
    
    void Recover()
    {
        moveTimer = moveTime;
        Now_Move = false;
        Debug.Log("如果一直在运行这个，那就动不了了");
        transform.position = originalPos;
        gameObject.SetActive(false);
    }
    
}
