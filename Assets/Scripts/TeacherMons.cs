using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
public class TeacherMons : Enemy
{
    #region 定义变量

    [Header("一些基本设置")]
    public Text my_Text;
    public Rigidbody2D Rig;
    public bool IsOnGround;
    public int damage_to_player;
    private int real_damage_to_player;
    private PlayerHealth playerHealth;
    public GameObject player;
    private Transform playerTransform;
    public bool Hitted = false;

    [Header("与老师使用技能相关")]
    public int RandomShootIndex;
    public bool CanRandom = true;
    public bool CanAttack = true;

    [Header("与老师怪跳跃相关")] 
    //此为老师怪移动的左边界
    public GameObject leftBond;
    
    //此数值设置为每一段applyJumpTime后运行一次ApplyJump（每一次ApllyJump会运行多次JumpOnce）
    public float applyJumpTime;
    private float applyJumpTimer;
    public bool CanApplyJump = true;
    
    //将此数值设置为upTime的整数倍，即为设定当前跳几次
    public float jumpTime;
    private float jumpTimer;

    public float upTime;
    private float upTimer;
    public float airTime;
    private float airTimer;

    public float y_jump_up;
    public float y_jump_down;

    public float jump_in_x;
    private float x_jump;
    
    //用于判断player与自身当前相对位置
    public float whereplayer;
    [Header("老师怪射击环绕呆子")] 
    public GameObject TriggerIdiotPrefab;
    private bool CanshootTrigger = true;

    [Header("老师怪射击平行呆子")] 
    public float shootTime;
    private float shootTimer;
    public GameObject ShootingIdiotPrefab;
    public float appeardown;
    public float appearleft;
    public Vector2 appearPos;

    [Header("老师怪觉得你真棒")] 
    public bool CanShootGood = false;
    public GameObject YouAreGoodPrefab;
    public GameObject Hearts;
    
    #endregion
    private void Awake()
    {
        real_damage_to_player = damage_to_player;
        
        Hearts = GameObject.Find("Heart");
        player = GameObject.Find("Player");
        
        upTimer = upTime;
        airTimer = airTime;
        jumpTimer = jumpTime;
        applyJumpTimer = applyJumpTime;
        shootTimer = shootTime;
        
        Rig = GetComponent<Rigidbody2D>();
        
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void FixedUpdate()
    {
        IsOnGround = OnGround();

        BossAttackLogic();

        OnHit();

        shootYouAreGood();

        if (CanShootGood)
        {
            x_jump = -jump_in_x;
        }
    }
    
    
    void shootTriggerIdiot()
    {
        if (CanshootTrigger)
        {
            for (float i = 0; i < 360f; i += 30f)
            {
                SoundManager.playHWMonsDownClip();
                Instantiate(TriggerIdiotPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, i)));
            }

            CanshootTrigger = false;
        }

        CanshootTrigger = true;
    }

    void shootShootingIdiot()
    {
        if (shootTimer < 0)
        {
            appearPos = new Vector2(transform.position.x - appearleft, transform.position.y - appeardown);
            Instantiate(ShootingIdiotPrefab, appearPos, transform.rotation);
            shootTimer = shootTime;
        }
        else if (shootTimer >= 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    void shootYouAreGood()
    {
        if (CanShootGood)
        {
            Rig.velocity = new Vector2(-2f * x_jump, 0);
            if (shootTimer < 0)
            {
                appearPos = new Vector2(transform.position.x - appearleft, transform.position.y - appeardown);
                Instantiate(YouAreGoodPrefab, appearPos, transform.rotation);
                shootTimer = shootTime;
            }
            else if (shootTimer >= 0)
            {
                shootTimer -= Time.deltaTime;
                StartCoroutine(WaitKillSelf());
            }
        }
    }

    void JumpOnce()
    {
        TrackPlayerPos();
        if (upTimer >= 0)
        {
            Rig.velocity = new Vector2(x_jump, y_jump_up);
            upTimer -= Time.deltaTime;
        }

        else if (upTimer < 0 && airTimer >= 0)
        {
            Rig.velocity = new Vector2(x_jump, 0);
            airTimer -= Time.deltaTime;
        }
        
        else if (airTimer < 0 && upTimer < 0 && !IsOnGround)
        {
            Rig.velocity = new Vector2(x_jump, -y_jump_down);
        }
        
        else if (upTimer < 0 && airTimer < 0 && IsOnGround)
        {
            //Rig.velocity = Vector2.zero;
            upTimer = upTime;
            airTimer = airTime;
            //跳完一次之后就不要再跳了
            shootTriggerIdiot();
        }
    }

    void ApplyJump()
    {
        if (CanApplyJump)
        {
            if (jumpTimer >= 0)
            {
                JumpOnce();
                jumpTimer -= Time.deltaTime;
            }

            else if (jumpTimer < 0)
            {
                //跳完一套过后停止
                CanApplyJump = false;
                jumpTimer = jumpTime;
            }
        }
    }


    void BossAttackLogic()
    {
        if (CanAttack)
        {
            //Debug.Log("能用跳的时间" + applyJumpTimer);
            if (CanRandom)
            {
                RandomShootIndex = Random.Range(0, 5);
                Debug.Log(RandomShootIndex);
                CanRandom = false;
            }

            if (applyJumpTimer >= 0)
            {
                if (RandomShootIndex == 0 || RandomShootIndex == 4 || RandomShootIndex == 3)
                {
                    ApplyJump();
                    applyJumpTimer -= Time.deltaTime;
                }

                else if (RandomShootIndex == 1 || RandomShootIndex == 2)
                {
                    shootShootingIdiot();
                    applyJumpTimer -= Time.deltaTime;
                }
            }

            else if (applyJumpTimer < 0)
            {
                //定时时间到后，恢复可以跳的状态
                SoundManager.playTeacherMonsComingClip();
                CanRandom = true;
                CanApplyJump = true;
                applyJumpTimer = applyJumpTime;
            }
        }
    }

    void TrackPlayerPos()
    {
        whereplayer = transform.position.x - playerTransform.position.x;
        if (!CanShootGood && transform.position.x > leftBond.transform.position.x + 2f)
        {
            if (whereplayer > 0)
            {
                x_jump = -jump_in_x;
            }
            else if (whereplayer <= 0)
            {
                x_jump = jump_in_x;
            }
        }
        
        else if (!CanShootGood && transform.position.x <= leftBond.transform.position.x + 2f)
        {
            x_jump = 0;
        }
    }
    void OnHit()
    {
        if (Hitted)
        {
            real_damage_to_player = 0;
            CanRandom = true;
            CanApplyJump = false;
            applyJumpTimer = applyJumpTime;
            Rig.velocity = new Vector2(-x_jump, 0);
            StartCoroutine(OnHitwait());
        }
    }

    IEnumerator OnHitwait()
    {
        yield return new WaitForSeconds(5f);
        Hitted = false;
        real_damage_to_player = damage_to_player;
    }

    IEnumerator WaitKillSelf()
    {
        Rig.velocity = new Vector2(-2f * x_jump, 0);
        yield return new WaitForSeconds(7f);
        SoundManager.playBossDefeatClip();
        my_Text.text = "";
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player_Old"))
        {
            my_Text.text = "佬是什么，我只喜欢大佬";
        }
        
        else if (other.gameObject.CompareTag("Player_Old"))
        {
            my_Text.text = "佬是什么，我只喜欢大佬";
            Hitted = true;
            CanAttack = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player_Old"))
        {
            my_Text.text = "";
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_Old"))
        {
            SoundManager.playQustionClip();
            my_Text.text = "佬是什么，我只喜欢大佬";
            Hitted = true;
            CanAttack = false;
        }

        else if (other.gameObject.CompareTag("Big_Old"))
        {
            SoundManager.playHitHWMonsClip();
            Hearts.GetComponent<Heart_Likes>().IsBigOld = true;
            my_Text.text = "哇是大佬诶，好牛，好喜欢";
            CanAttack = false;
            CanShootGood = true;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player_Old"))
        {
            my_Text.text = "佬是什么，我只喜欢大佬";
            Hitted = true;
            CanAttack = false;
        }
        
        else if (other.gameObject.CompareTag("Big_Old"))
        {
            Hearts.GetComponent<Heart_Likes>().IsBigOld = true;
            player.GetComponent<PlayerCTRL>().OffRideFromOther();
            Debug.Log("Woc, 大佬， 牛X！！！");
            CanAttack = false;
            CanShootGood = true;
        }
    }
}
