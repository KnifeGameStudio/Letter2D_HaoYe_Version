using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    
    //设置总血量
    public bool CanBeHurt = true;
    public int maxi_helath;
    public int health;

    private SpriteRenderer Player_SR;
    private Color originalColor;
    public float FlashTime;

    //血量 < 0 过后触发倒下动画
    private Animator anim;
    public float dieTime;

    //流血和回血粒子特效生成
    private PlayerCTRL playerctrl;
    public GameObject BloodEffect;
    public GameObject RecoverEffect;
    
    void Start()
    {
        health = maxi_helath;
        Player_SR = GetComponent<SpriteRenderer>();
        originalColor = Player_SR.color;
        anim = GetComponent<Animator>();
        playerctrl = GetComponent<PlayerCTRL>();
    }
    
    void Update()
    {
        HealthBar.HealthCurrent = health;
    }

    public void DamagePlayer(int damage)
    {
        if (CanBeHurt)
        {
            SoundManager.playManHurtClip();
            health -= damage;
            Instantiate(BloodEffect, new Vector2(transform.position.x, transform.position.y - 1f), Quaternion.identity);
            FlashColor(FlashTime);
            if (health < 0)
            {
                health = 0;
            }

            HealthBar.HealthCurrent = health;
            if (health <= 0)
            {
                PlayerPrefs.SetString("Stage", SceneManager.GetActiveScene().name);
                anim.SetTrigger("Man_Dead");
                Debug.Log("你挂了");
                playerctrl.Rig.velocity = Vector2.zero;
                playerctrl.CanInput = false;
                Invoke("KillPlayer", dieTime);
            }
        }
    }

    public void HealPlayer(int heal)
    {
        if (health < maxi_helath)
        {
            SoundManager.playHealClip();
            Instantiate(RecoverEffect, new Vector2(transform.position.x, transform.position.y- 1f), Quaternion.identity);
            health += heal;
        }

        if (health >= maxi_helath)
        {
            health = maxi_helath;
        }
    }
    
    void KillPlayer()
    {
        
        //Destroy(gameObject);
        playerctrl.CanInput = true;
        SceneManager.LoadScene("Main_Menu");
    }
    
    void FlashColor(float time)
    {
        CanBeHurt = false;
        originalColor.a = 0.48f;
        Player_SR.color = originalColor;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        originalColor.a = 1.0f;
        Player_SR.color = originalColor;
        CanBeHurt = true;
    }
}
