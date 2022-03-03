using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Home_LivingRoom_B_Music : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Home_LivingRoom_B" 
           && SceneManager.GetActiveScene().name != "Home_ParentsRoom" 
           && SceneManager.GetActiveScene().name != "TempEnding")
        {
            Destroy(gameObject);
        }
    }
}
