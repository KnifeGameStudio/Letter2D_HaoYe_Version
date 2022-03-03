using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Settings_GoBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoBack_To_Main()
    {
        SceneManager.LoadScene("Scenes/Menu_Scenes/Main_Menu");
    }
}
