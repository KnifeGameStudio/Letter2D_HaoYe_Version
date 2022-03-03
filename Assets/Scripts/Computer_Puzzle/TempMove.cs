using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMove : MonoBehaviour
{
    public GameObject chess;
    public GameObject code;
    public GameObject xiang;
    public GameObject dui;

    int speed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed*Input.GetAxis("Horizontal")*Time.deltaTime, 0, 0);
        transform.position += new Vector3(0, speed*Input.GetAxis("Vertical")*Time.deltaTime, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "exit")
        {
            SceneManager.LoadScene("Home_BookRoom");
        }

        if(collision.transform.name == "chess")
        {
            chess.SetActive(true);
            code.SetActive(false);

            xiang.SetActive(true);
        }

        if (collision.transform.name == "code")
        {
            chess.SetActive(false);
            code.SetActive(true);
            if (xiang.GetComponent<Xiang>().paring)
            {
                dui.SetActive(true);
                xiang.SetActive(true);
            }
            else if (!xiang.GetComponent<Xiang>().paring)
            {
                dui.SetActive(false);
                xiang.SetActive(false);
            }
        }
    }
}
