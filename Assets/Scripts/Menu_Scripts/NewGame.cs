using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_New_Game()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Tutor_Scene");
    }
    
}
