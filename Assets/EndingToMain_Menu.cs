using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndingToMain_Menu : MonoBehaviour
{
    public float changeTimer;
    public float changeTime;
    void Start()
    {
        changeTimer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeTimer <= 0)
        {
            SceneManager.LoadScene("Main_Menu");
        }
        else
        {
            changeTimer -= Time.deltaTime;
        }
    }
}
