using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    public AudioClip normal_bgm;
    public AudioClip boss_bgm;

    public bool Met_Boss;
    void Start()
    {
        GetComponent<AudioSource>().clip = normal_bgm;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBGM();
    }

    public void ChangeBGM()
    {
        if (!Met_Boss)
        {
            GetComponent<AudioSource>().clip = normal_bgm;
        }
        else if (Met_Boss)
        {
            GetComponent<AudioSource>().clip = boss_bgm;
            /*StartCoroutine(bossBgmWait());*/
        }
    }

    /*IEnumerator bossBgmWait()
    {
        GetComponent<AudioSource>().Pause();
        yield return new WaitForSeconds(3f);
        GetComponent<AudioSource>().clip = boss_bgm;
        GetComponent<AudioSource>().Play();
    }*/
}
