using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Parent_Give_Problem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FallingProblemPrefab;

    public GameObject progressChek2;

    public GameObject drawerunder;
    
    public float appearDown;
    public float appearLeft;
    Vector2 appearPlace;
    public bool Now_Move = false;
    public float moveTime;
    private float moveTimer;
    public bool problemGiven;
    public bool problemExist;
    
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
            drawerunder.GetComponent<Drawer_UnderBed>().ParentComes = true;
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
            StartCoroutine(GiveProblemWait());
        }
    }

    IEnumerator GiveProblemWait()
    {
        velocity = Vector3.zero;
        yield return new WaitForSeconds(3f);
        if (!problemGiven && progressChek2 != null && !problemExist)
        {
            appearPlace = new Vector2(transform.position.x - appearLeft, transform.position.y - appearDown);
            Instantiate(FallingProblemPrefab, appearPlace, transform.rotation);
            problemGiven = true;
            problemExist = true;
        }
        player.GetComponent<PlayerCTRL>().CanInput = true;
        StartCoroutine(GobackWait());
    }

    IEnumerator GobackWait()
    {
        yield return new WaitForSeconds(2f);
        Recover();
    }
    
    void Recover()
    {
        moveTimer = moveTime;
        problemGiven = false;
        Now_Move = false;
        Debug.Log("如果一直在运行这个，那就动不了了");
        transform.position = originalPos;
        gameObject.SetActive(false);
    }

}
