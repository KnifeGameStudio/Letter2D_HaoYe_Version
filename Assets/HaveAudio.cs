using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveAudio : MonoBehaviour
{
    public GameObject AC;
    void Start()
    {
        if (GameObject.Find("Music") == null)
        {
            AC.GetComponent<AudioSource>().Play();
        }
        else
        {   
            AC.GetComponent<AudioSource>().Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
