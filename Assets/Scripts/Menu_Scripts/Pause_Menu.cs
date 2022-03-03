using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause_Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isPaused;
    public GameObject PauseMenuUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void BacktoMain()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        PlayerPrefs.SetString("Stage", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Main_Menu");
    }

}
