using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerCTRL : MonoBehaviour
{
    //--------------------------------------- 定义全局变量 ----------------------------------------
    #region 定义变量

    [Header("玩家是否控制")] 
    public bool CanInput;
    
    [Header(("和UI画布提示相关"))]
    public Text my_Text;

    public bool PlayMagic = false;
    
    [Header("和Ride相关")] 
    //关于是否在Ride和Ride到谁的变量
    
    public bool Try_To_Ride;
    public bool Riding;
    public bool CanRide;
    //此处为一定时间内未找到合适的骑的对象则退出准备骑的状态
    public float Try_Riding_Time_Limit;
    private float timer;
    public Image ScreenFlash;
    public float FlashTime;
    public Color flashColor;
    private Color defaultColor;
    private bool Is_Flash1 = true;
    private bool Is_Flash2 = true;
    
    [Header("移动")]
    public bool CanMove = true;
    public float WalkSpeed;
    public float AccelerationTime;
    public float DecelerationTime;

    [Header("跳跃")]
    public bool CanJump = true;
    public float JumpingSpeed;
    public float FallMultiplier;
    public float LowJumpMultiplier;

    [Header("判定是否在地上")]
    public Vector2 PointOffset;
    public Vector2 Size;
    public LayerMask GroundLayerMask;
    public bool GravityModifier = true;

    
    [Header("自身")]
    public Rigidbody2D Rig;
    private Animator Anim;
    private SpriteRenderer SR;

    public float velocityX;

    private bool IsJumping;
    private bool IsOnGround;

    /*[Header("SoundEffects")]
    public AudioClip AC;
    private int isplaying = 0;*/
    //每次播放时把isplaying改成1，否则音效容易多次播放
    #endregion
    
    //在几个特定位置，刚load scene，回到上一次储存的来到此scene时的位置, 放到start或者oneable里面
    void goToLastPos()
    {
        if (PlayerPrefs.HasKey("Home_BookRoom_x")){
            transform.position = new Vector3(PlayerPrefs.GetFloat("Home_BookRoom_x")
                , PlayerPrefs.GetFloat("Home_BookRoom_y"), PlayerPrefs.GetFloat("Home_BookRoom_z"));

            PlayerPrefs.DeleteKey("Home_BookRoom_x");
            PlayerPrefs.DeleteKey("Home_BookRoom_y");
            PlayerPrefs.DeleteKey("Home_BookRoom_z");
        }
        if (PlayerPrefs.HasKey("Home_SelfRoom_x") && SceneManager.GetActiveScene().name == "Home_SelfRoom"){
            transform.position = new Vector3(PlayerPrefs.GetFloat("Home_SelfRoom_x")
                , PlayerPrefs.GetFloat("Home_SelfRoom_y"), PlayerPrefs.GetFloat("Home_SelfRoom_z"));

            PlayerPrefs.DeleteKey("Home_SelfRoom_x");
            PlayerPrefs.DeleteKey("Home_SelfRoom_y");
            PlayerPrefs.DeleteKey("Home_SelfRoom_z");
        }
    }
    
    void Start()
    {
        goToLastPos();

        defaultColor = ScreenFlash.color;
        timer = Try_Riding_Time_Limit;
        GroundLayerMask = LayerMask.GetMask("Ground", "CanNotRideThings","CanRideThings");
        Rig = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        IsOnGround = OnGround();
        LeftRightMove();
        Jump();
        GravityAdjustment();
        CallForOffRide();
        TryRide();
    }

    //--------------------------------------- 有关操作的代码 ----------------------------------------
    
    #region 有关行动操作的代码
    
    private bool OnGround()
    {
        Collider2D Coll= Physics2D.OverlapBox((Vector2)transform.position + PointOffset,Size,0,GroundLayerMask);
        if (Coll != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube((Vector2)transform.position + PointOffset,Size);
    }
    private void LeftRightMove()
    {
        if (CanMove && CanInput)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                Rig.velocity =
                    new Vector2(
                        Mathf.SmoothDamp(Rig.velocity.x, WalkSpeed * Time.fixedDeltaTime * 60, ref velocityX,
                            AccelerationTime), Rig.velocity.y);
                if (!Riding){SR.flipX = false;}
                Anim.SetFloat("Man_Walk", 1f);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                Rig.velocity =
                    new Vector2(
                        Mathf.SmoothDamp(Rig.velocity.x, WalkSpeed * Time.fixedDeltaTime * 60 * -1, ref velocityX,
                            AccelerationTime), Rig.velocity.y);
                if (!Riding){SR.flipX = true;}
                Anim.SetFloat("Man_Walk", 1f);
            }
            else if (Input.GetAxis("Horizontal") == 0)
            {
                Rig.velocity = new Vector2(Mathf.SmoothDamp(Rig.velocity.x, 0, ref velocityX, DecelerationTime),
                    Rig.velocity.y);
                Anim.SetFloat("Man_Walk", 0);
            }
        }
    }
    private void Jump()
    {
        if (CanJump && CanInput)
        {
            if (!IsOnGround)
            {
                Anim.SetFloat("Man_Walk", 0);
            }
            
            if (Input.GetAxis("Jump") == 1 && IsJumping == false)
            {
                Rig.velocity = new Vector2(Rig.velocity.x, JumpingSpeed);
                IsJumping = true;
                Anim.SetBool("Jumping", true);

            }

            else if (IsOnGround && Input.GetAxis("Jump") == 1)
            {
                Anim.SetBool("Jumping", false);
            }
            
            else if (IsOnGround && Input.GetAxis("Jump") == 0)
            {
                IsJumping = false;
                Anim.SetBool("Jumping", false);
            }
        }
    }
    private void GravityAdjustment()
    {
        if (GravityModifier)
        {
            if (Rig.velocity.y < 0) // 当玩家下坠时候
            {
                Rig.velocity +=
                    Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) *
                    Time.fixedDeltaTime; // (加速下坠)
                //Rig.velocity = new Vector2(Rig.velocity.x, -JumpingSpeed);
            }
            else if (Rig.velocity.y > 0 && Input.GetAxis("Jump") != 1
            ) //当玩家上升且没有按下跳跃键时
            {
                Rig.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) *
                                Time.fixedDeltaTime; // (减速上升)

            }
        }
    }
    
    #endregion
    
    //------------------------------------- 有关骑与不骑的代码 ----------------------------------------

    #region 和OffRide相关的代码
    
    //此处为在以下两种状况下调用OffRide函数（即，在下面两种情况下就不骑了）
    void CallForOffRide()
    {
        /*此条件为当玩家已经骑上某样东西，且玩家按下了键盘上“F”按键准备不再骑时，此时重新打开Player的SR和boxcollider，
        并且将已经Ride过的物体的爷爷改成null，同时为了避免与原物体冲突，
        将player向左移动一段距离（因为单人旁一般都会是在左边，如之后有人字头等再另行考虑）*/
        
        if (CanInput && !Try_To_Ride && Riding && IsOnGround && (Input.GetAxis("OffRide") == 1))
        {
            OffRideFromOther();
            PlayMagic = false;
        }
        
        //此条件为当玩家在规定时间内未找到合适骑的对象时
        else if (Try_To_Ride && !Riding)
        {
            FixedTimeOffRide();
            PlayMagic = false;
        }
    }
    
    //函数将在玩家按下键盘上“E”键准备尝试骑的时候，开始进行计时
    void FixedTimeOffRide()
    {
        timer -= Time.fixedDeltaTime;
        Debug.Log("开始计时了" + timer);

        if (timer > 0)
        {
            if (timer <= 2f && Is_Flash1)
            {
                Is_Flash1 = false;
                StartCoroutine(Flash());
            }
            else if (timer <= 1f && Is_Flash2)
            {
                Is_Flash2 = false;
                StartCoroutine(Flash());
            }
        }

        else if (timer <= 0)
        {
            Debug.Log("时间到了！！！这么久都没找到骑的？");
            my_Text.text = "时间到了! 没找到合适的字";
            OffRide();
            //重置倒计时
            StartCoroutine(Flash());
            timer = Try_Riding_Time_Limit;
            Is_Flash1 = true;
            Is_Flash2 = true;
        }
    }
    
    // 此处为倒计时只剩2秒时的屏幕闪烁
    IEnumerator Flash()
    {
        Debug.Log("我开始闪了");
        ScreenFlash.color = flashColor;
        yield return new WaitForSeconds(FlashTime);
        ScreenFlash.color = defaultColor;
    }
    
    //不再骑了，此时玩家不再Try_To_Ride,Riding的状态也关掉，自身的tag也从单人旁变回Player，也暂时不可对键盘进行输入（动画结束后恢复）
    //这个OffRide是玩家变成单人旁，尝试Ride但没找到之后运行的常规OffRide
    void OffRide()
    {
        CanInput = false;
        //在刚刚结束骑时，便需要将tag改回Player
        gameObject.tag = "Player";
        /*GetComponent<BoxCollider2D>().enabled = true;*/
        Try_To_Ride = false;
        Anim.SetBool("Try_Ride",false);
        Rig.velocity = Vector2.zero;
        Riding = false;
        StartCoroutine(OffRideDelay());
    }
    
    //等待从单人旁变回人字的动画的时间
    IEnumerator OffRideDelay()
    {
        yield return new WaitForSeconds(1.5f);
        CanInput = true;
        CanJump = true;
        CanMove = true;
        CanRide = true;
    }

    //此为已经成功Ride到其他物体上的时候，的另外一种需要改变更多的OffRide的方式，同时被设置成public可被其他物体调用
    public void OffRideFromOther()
    {
        OffRide();
        SR.enabled = true;
        if (transform.GetChild(0).GetChild(0).gameObject)
        {
            transform.GetChild(0).GetChild(0).GetComponent<CanBeRided>().OnRide = false;
            transform.GetChild(0).GetChild(0).parent = null;
        }
        else
        {
            Debug.Log("我倒，这个孙子不见了");
        }
        transform.position = new Vector2(transform.position.x-2f, transform.position.y);
        GetComponent<BoxCollider2D>().enabled = true;
    }
    
    #endregion

    #region 和Ride相关的代码

    
    //-------------------------------- 尝试变成单人旁去骑的代码 ----------------------------------
    //此时变成单人旁，尝试去骑
    void TryRide()
    {
        if (CanInput && !Try_To_Ride && !Riding && CanRide && IsOnGround && (Input.GetAxis("Ride") == 1))
        {
            CanRide = false;
            Anim.SetBool("Try_Ride",true);
            Rig.velocity = Vector2.zero;
            CanInput = false;
            //此处等待动画延迟时，可能会遇到动画看起来结束了，但实际上协程还未结束，因此需要在此处关掉CanRide，一定要等协程结束才能打开CanRide
            StartCoroutine(TryRideDelay());
        }
    }

    //等待人字变成单人旁的动画的时间
    IEnumerator TryRideDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Try_To_Ride = true;
        gameObject.tag = "Single_Man";
        CanInput = true;
        CanMove = true;
        CanJump = true;
        //此处，一定要等协程结束才能打开CanRide
        CanRide = true;
    }
    
    //-------------------------------- 当碰撞到可骑或不可骑物体时 ----------------------------------

    //此为真正碰撞到可以骑或者不可以骑的对方物体时进行的Ride函数
    void Ride(Collider2D other)
    {
        if (CanRide)
        {
            if (gameObject.CompareTag("Single_Man") && other.gameObject.layer == 8)
            {
                /*此时说明自己是“单人旁”，而对方是可以骑的，此时关掉自身的boxcollider和sprite，
                 关掉状态“试图骑别人”，因为此时已经骑了，打开状态“正在骑”
                并告诉对方，对方被骑了
                并且自身移动到对方的位置，并且把对方变成自己的儿子RidingOn的儿子（也就是变成自己的孙子）
                并且自己的tag变成Player_ 加上对方的标签，如此其他物体便可以和此独一无二的的tag进行交互
                */
                if (!PlayMagic)
                {
                    SoundManager.playMagicClip();
                    PlayMagic = true;
                }
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                my_Text.text = "好神奇，我找到了合适的字！";
                //重置计时时间
                timer = Try_Riding_Time_Limit;
                Is_Flash1 = true;
                Is_Flash2 = true;
                other.GetComponent<CanBeRided>().OnRide = true;
                GetComponent<BoxCollider2D>().enabled = false;
                SR.enabled = false;
                Try_To_Ride = false;
                Riding = true;
                CanRide = false;
                transform.position = other.transform.position;
                other.transform.parent = gameObject.transform.GetChild(0).transform;
                gameObject.tag = "Player_" + other.tag;
            }
            else if (gameObject.CompareTag("Single_Man") && other.gameObject.layer == 7)
            {
                //重置计时时间
                timer = Try_Riding_Time_Limit;
                Is_Flash1 = true;
                Is_Flash2 = true;
                my_Text.text = "这个字不合适哦";
                OffRide();
            }
        }
    }
    //这是用Trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        Ride(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        my_Text.text = "";
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (gameObject.CompareTag("Single_Man") && other.gameObject.layer == 7)
        {
            //重置计时时间
            timer = Try_Riding_Time_Limit;
            Is_Flash1 = true;
            Is_Flash2 = true;
            OffRide();
        }
    }

    /*private void OnCollisionExit(Collision other)
    {
        my_Text.text = "";
    }*/

    #endregion
}
