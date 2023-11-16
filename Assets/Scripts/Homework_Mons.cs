using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Homework_Mons : Enemy
{

    public bool IsOnGround;
    public int damage_to_player;
    private int real_damage_to_player;
    private PlayerHealth playerHealth;
    public GameObject player;
    public bool Hitted = false;


    [Header("与作业怪跳跃相关")] public bool canJump = true;
    public Rigidbody2D Rig;
    public float jumpWaitTime;
    private float jumpTimer;
    public SpriteRenderer SR;

    public float upTime;
    private float upTimer;
    public float airTime;
    private float airTimer;
    public float downTime;
    private float downTimer;
    public float jump_in_x;

    private float x_jump;

    //判断player与自身当前相对位置
    public float whereplayer;

    public bool CanDownEffect = false;
    public GameObject DownEffect;


    [Header("与生成新的有关故事书")]
    
    public GameObject StoryBookPrefab;
    public float StoryBook_appearYpos;
    private Vector2 Do_appearPos;
    public GameObject leftBound;
    public GameObject RightBound;


    [Header("与作业怪射击相关")] 
    
    public GameObject ProblemPrefab;
    public GameObject youAreGoodPrefab;
    public float prob_radius;
    public float crazily_prob_waitime;
    private float crazily_prob_waitimer;
    public float prob_waitTime = 1.7f;
    Vector2 appearPos1;
    Vector2 appearPos2;
    private float negShootSpeed = -10f;
    private float posShootSpeed = 10f;

    private bool shoot1 = true;
    private bool shoot2 = true;

    private Transform playerTransform;

    public float apperdown;


    private void Awake()
    {
        real_damage_to_player = damage_to_player;
        player = GameObject.Find("Player");
        upTimer = upTime;
        airTimer = airTime;
        downTimer = downTime;
        jumpTimer = jumpWaitTime;
        Rig = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        IsOnGround = OnGround();
        
        if (health > 0.1 && !Hitted)
        {
            JumpOnce();
        }
        else if (health <= 0)
        {
            KillSelf();
        }

        /*if (IsOnGround)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (!IsOnGround)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }*/

        OnHit();
        whereplayer = transform.position.x - playerTransform.position.x;
        if (whereplayer > 0)
        {
            SR.flipX = false;
            x_jump = -jump_in_x;
        }
        else if (whereplayer <= 0)
        {
            x_jump = jump_in_x;
        }

        
        /*Debug.Log("negspeed" +negShootSpeed);
        Debug.Log("posspeed" +posShootSpeed);*/
        //apply_ShootProblem();

        //EveryWaitTimeJump();
    }

    void PlayDownEffect()
    {
        if (CanDownEffect)
        {
            SoundManager.playHWMonsDownClip();
            Instantiate(DownEffect, new Vector2(transform.position.x, transform.position.y - 3f - apperdown),
                Quaternion.identity);
            CanDownEffect = false;
        }
    }

    private void apply_ShootProblem()
    {
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance <= prob_radius)
            {
                crazilyShootProblem();
            }
            else
            {
                JumpOnce();
            }
        }
    }

    void crazilyShootProblem()
    {
        if (whereplayer < 0)
        {
            ProblemPrefab.GetComponent<ShootingProblem>().speed = negShootSpeed;
        }
        else if (whereplayer >= 0)
        {
            ProblemPrefab.GetComponent<ShootingProblem>().speed = posShootSpeed;
        }

        appearPos2 = new Vector2(transform.position.x, transform.position.y - apperdown);

        if (crazily_prob_waitimer <= 0)
        {
            Instantiate(youAreGoodPrefab, appearPos2, transform.rotation);
            crazily_prob_waitimer = crazily_prob_waitime;
        }

        else
        {
            crazily_prob_waitimer -= Time.deltaTime;
        }
    }

    void shootDoStory()
    {
        if (playerTransform.position.x >= leftBound.transform.position.x + 8f 
            && playerTransform.position.x <= RightBound.transform.position.x - 10f)
        {
            Do_appearPos = new Vector2(playerTransform.position.x, transform.position.y + StoryBook_appearYpos);
        }
        
        else if(playerTransform.position.x < leftBound.transform.position.x + 8f)
        {
            Do_appearPos = new Vector2(leftBound.transform.position.x + 8f,
                transform.position.y + StoryBook_appearYpos);
        }
        
        else if(playerTransform.position.x > RightBound.transform.position.x - 10f)
        {
            Do_appearPos = new Vector2(RightBound.transform.position.x - 10f,
                transform.position.y + StoryBook_appearYpos);
        }

        if (GameObject.FindGameObjectsWithTag("Story").Length == 0 &&
            GameObject.FindGameObjectsWithTag("Player_Story").Length == 0)
        {
            Instantiate(StoryBookPrefab, Do_appearPos, transform.rotation);
        }
    }

    void shootProblem()
    {
        if (whereplayer < 0)
        {
            ProblemPrefab.GetComponent<ShootingProblem>().speed = negShootSpeed;
        }
        else if (whereplayer >= 0)
        {
            ProblemPrefab.GetComponent<ShootingProblem>().speed = posShootSpeed;
        }

        appearPos1 = new Vector2(transform.position.x, transform.position.y + apperdown);
        appearPos2 = new Vector2(transform.position.x, transform.position.y - apperdown);

        if (prob_waitTime <= 1.6 && shoot1)
        {
            PlayDownEffect();
            Instantiate(ProblemPrefab, appearPos2, transform.rotation);
            shoot1 = false;
        }

        if (prob_waitTime <= 0.8 && shoot2)
        {
            Instantiate(ProblemPrefab, appearPos1, transform.rotation);
            shootDoStory();
            shoot2 = false;
        }
        else if (prob_waitTime <= 0)
        {
            Instantiate(ProblemPrefab, appearPos2, transform.rotation);
            prob_waitTime = 1.7f;
            shoot1 = true;
            shoot2 = true;

            upTimer = upTime;
            airTimer = airTime;
            downTimer = downTime;
        }
        else
        {
            prob_waitTime -= Time.deltaTime;
        }
    }

    void JumpOnce()
    {
        if (canJump)
        {
            /*Debug.Log("DownTime Is" + downTimer);
            Debug.Log("UpTime Is " + upTimer);*/

            if (upTimer <= 6 && upTimer > 5)
            {
                Rig.velocity = new Vector2(x_jump, 10f);
                upTimer -= Time.deltaTime;
            }
            else if (upTimer > 6)
            {
                upTimer -= Time.deltaTime;
            }

            if (airTimer <= 6 && airTimer > 5)
            {
                Rig.velocity = new Vector2(x_jump, 0);

                airTimer -= Time.deltaTime;
            }
            else if (airTimer > 6)
            {
                airTimer -= Time.deltaTime;
            }

            if (downTimer <= 6 && downTimer > 5)
            {
                Rig.velocity = new Vector2(x_jump, -30f);
                downTimer -= Time.deltaTime;
            }
            else if (downTimer > 6)
            {
                downTimer -= Time.deltaTime;
            }

            if (upTimer <= 5 && downTimer <= 5.8f)
            {
                CanDownEffect = true;

                shootProblem();
            }
        }
    }

    void OnHit()
    {
        if (Hitted)
        {
            canJump = false;
            real_damage_to_player = 0;
            Rig.velocity = new Vector2(-x_jump, 0);
            StartCoroutine(OnHitwait());
        }
    }

    IEnumerator OnHitwait()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(OnHitAttack());
    }

    IEnumerator OnHitAttack()
    {
        crazilyShootProblem();
        yield return new WaitForSeconds(2f);
        canJump = true;
        Hitted = false;
        real_damage_to_player = damage_to_player;
    }

    void KillSelf()
    {
        StartCoroutine("WaitKillSelf");
    }

    IEnumerator WaitKillSelf()
    {
        yield return new WaitForSeconds(3f);
        SoundManager.playBossDefeatClip();
        PlayerPrefs.SetFloat("defeat_HWMons", 1f);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_Story") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            TakeDamage(being_damaged);
            if (other.name == "Do")
            {
                player.GetComponent<PlayerCTRL>().OffRideFromOther();
                Destroy(other.transform.parent.gameObject, 0.5f);
                Hitted = true;
                SoundManager.playHitHWMonsClip();
            }

        }

        else if ((other.gameObject.CompareTag("Player")) && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            if (playerHealth != null)
            {
                //this damage is the damage from enemy to the player
                playerHealth.DamagePlayer(real_damage_to_player);
            }
        }
    }


}

