using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckCode : MonoBehaviour
{
    public GameObject error;
    public GameObject answer;
    public GameObject success;
    public GameObject Code;
    
    public GameObject RidingEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("computer_Puzzle", 0f) == 1f)
        {
            error.SetActive(false);
            answer.SetActive(true);
            StartCoroutine(Success_And_Go());
            Code.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetFloat("computer_Puzzle", 0f) != 1f)
        {
            var name = collision.transform.name;
            if (name == "Xiang" || name == "x1" || name == "x2" || name == "dui")
            {
                Instantiate(RidingEffectPrefab, transform.position,transform.rotation);
                error.SetActive(false);
                collision.gameObject.SetActive(false);
                answer.SetActive(true);
                SoundManager.playPuzzleSolvedClip();
                PlayerPrefs.SetFloat("computer_Puzzle", 1f);
                //active door
                StartCoroutine(Success_And_Go());
            }
        }
        
    }

    IEnumerator Success_And_Go()
    {
        success.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Home_BookRoom");
    }
}
