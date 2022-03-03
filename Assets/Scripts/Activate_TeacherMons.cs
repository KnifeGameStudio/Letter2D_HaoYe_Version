using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_TeacherMons : MonoBehaviour
{
    [Header("激活Boss后关上墙")] 
    public GameObject bgm1;
    public GameObject bgm2;
    public GameObject bgm3;
    
    public GameObject wall1;
    public GameObject boss;

    public GameObject player;

    public GameObject ActiveBossEffect;
    private float effecttimer = 0.1f;
    
    public float BossActiveTime;
    
    public bool bossActivated;
    public bool PlayerComing;

    public bool bossDefeat;
    public bool bossComing;

    void Start()
    {
        player = GameObject.Find("Player");
        PlayerComing = false;
    }

    // Update is called once per frame
    void Update()
    {
        BossExist();
        DownWall();
    }
    
    void BossExist()
    {
        if (boss != null)
        {
            bossDefeat = false;
        }
        else if (boss == null)
        {
            bossDefeat = true;
        }
    }

    void DownWall()
    {
        if (PlayerComing && !bossActivated && !bossDefeat)
        {
            wall1.SetActive(true);
            bgm1.SetActive(false);
            StartCoroutine(BossActiveWait());
        }
        
        else if (PlayerComing && bossActivated && !bossDefeat)
        {
            bgm2.SetActive(true);
            wall1.SetActive(true);
        }

        else if (PlayerComing && bossActivated && bossDefeat)
        {
            bgm2.SetActive(false);

            StartCoroutine(BossDefeatedWait());
        }
        
        else if(!PlayerComing)
        {
            wall1.SetActive(false);
        }
    }

    IEnumerator BossActiveWait()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerCTRL>().CanMove = false;
        player.GetComponent<PlayerCTRL>().CanJump = false;

        if (!bossComing)
        {
            bossComing = true;
            SoundManager.playTeacherMonsComingClip();
        }

        if (boss != null)
        {
            boss.SetActive(true);
            boss.GetComponent<TeacherMons>().CanAttack = false;
            Invoke("bossActiveEffect",0.5f);
        }
        yield return new WaitForSeconds(BossActiveTime);
        bossActivated = true;
        player.GetComponent<PlayerCTRL>().CanMove = true;
        player.GetComponent<PlayerCTRL>().CanJump = true;
        boss.GetComponent<TeacherMons>().CanAttack = true;
    }

    void bossActiveEffect()
    {
        if (effecttimer <= 0)
        {
            Instantiate(ActiveBossEffect, boss.transform.position, Quaternion.identity);
            effecttimer = 0.1f;
        }
        else
        {
            effecttimer -= Time.deltaTime;
        }
    }
    
    IEnumerator BossDefeatedWait()
    {
        yield return new WaitForSeconds(3f);
        wall1.SetActive(false);
        bgm3.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerComing = true;
        }
    }
}
