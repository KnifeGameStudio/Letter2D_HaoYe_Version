using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BeforeEverything : MonoBehaviour
{
    public float changeTimer;
    public float changeTime;
    void Start()
    {
        changeTimer = changeTime;
    }

    void Update()
    {
        if (changeTimer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            changeTimer -= Time.deltaTime;
        }
    }
}
