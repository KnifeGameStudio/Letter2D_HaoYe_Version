using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GlobalMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Opening" 
           && SceneManager.GetActiveScene().name != "Main_Menu" 
           && SceneManager.GetActiveScene().name != "Menu_About" 
           && SceneManager.GetActiveScene().name != "Menu_Settings" )
        {
            Destroy(gameObject);
        }
    }
}
