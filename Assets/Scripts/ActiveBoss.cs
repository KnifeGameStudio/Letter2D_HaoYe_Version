using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    [Header("检查有无此物体，如果无，才激活boss")] 
    public GameObject bossObject;
    //public GameObject bossDialogue;

    public GameObject bgm1;
    public GameObject bgm2;
    public GameObject bgm3;

    [Header("激活Boss后关上墙")]
    public GameObject wall1;
    public GameObject wall2;
    public GameObject boss;

    public GameObject player;

    public bool bossComingSound;
    
    public GameObject ActiveBossEffect;
    private float effecttimer = 0.1f;
    
    public float BossActiveTime;
    
    public bool bossActivated;
    public bool PlayerComing;

    public bool bossDefeat;
    

    void Start()
    {
        player = GameObject.Find("Player");
        PlayerComing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossObject == null)
        {
            BossExist();
            DownWall();
        }
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
            /*Destroy(bossDialogue);*/
        }
    }

    void DownWall()
    {
        if (PlayerComing && !bossActivated && !bossDefeat)
        {
            bgm1.SetActive(false);
            wall1.SetActive(true);
            wall2.SetActive(true);
            StartCoroutine(BossActiveWait());
        }
        
        else if (PlayerComing && bossActivated && !bossDefeat)
        {
            bgm2.SetActive(true);
            wall1.SetActive(true);
            wall2.SetActive(true);
        }

        else if (PlayerComing && bossActivated && bossDefeat)
        {
            bgm2.SetActive(false);
            StartCoroutine(BossDefeatedWait());

        }
        
        else if(!PlayerComing)
        {
            wall1.SetActive(false);
            wall2.SetActive(false);
        }
    }

    IEnumerator BossActiveWait()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerCTRL>().CanMove = false;
        player.GetComponent<PlayerCTRL>().CanJump = false;
        if (boss != null)
        {
            if (!bossComingSound)
            {
                bossComingSound = true;
                SoundManager.playHWMonsComingClip();
            }
            boss.SetActive(true);
            Invoke("bossActiveEffect",0.5f);
        }
        yield return new WaitForSeconds(BossActiveTime);
        bossActivated = true;
        player.GetComponent<PlayerCTRL>().CanMove = true;
        player.GetComponent<PlayerCTRL>().CanJump = true;
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
        wall2.SetActive(false);
        bgm3.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bossObject == null && other.CompareTag("Player"))
        {
            PlayerComing = true;
        }
    }
}
