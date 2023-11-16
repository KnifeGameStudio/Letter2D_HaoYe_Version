using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer_UnderBed : MonoBehaviour
{
public Text my_Text;
    public GameObject player;

    public GameObject HiddenThing;
    public bool Findit = false;

    public Vector3 initialVelocity;
    Vector3 velocity;
    public float moveTime;
    private float moveTimer;
    
    public bool DrawerIsOut;
    public bool DrawerCanMove;
    public bool CanInput = true;
    public bool ParentComes;
    
    void Start()
    {
        player = GameObject.Find("Player");
        velocity = initialVelocity;
    }

    IEnumerator FoundWait()
    {
        yield return new WaitForSeconds(1f);
        HiddenThing.gameObject.SetActive(true);
        HiddenThing.GetComponent<StoryBook_Trans>().OnFound = true;
    }

    void Update()
    {

        MoveDrawer();
        if (HiddenThing != null)
        {
            if (transform.GetChild(0).name == "StoryBook_Trans" && Findit)
            {
                StartCoroutine("FoundWait");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerCTRL>().CanRide = false;
            if (!DrawerIsOut && CanInput)
            {
                my_Text.text = "Press ↑ to operate the drawer(抽屉)";
                if (ParentComes)
                {
                    if (Input.GetAxis("Talk") == 1)
                    {
                        SoundManager.playDrawerClip();
                        Findit = true;
                        DrawerIsOut = true;
                        DrawerCanMove = true;
                        CanInput = false;
                    }
                }
                else if (!ParentComes)
                {
                    if (Input.GetAxis("Talk") == 1)
                    {
                        my_Text.text = "Mom and Dad haven't been here yet. Don't open the drawer yet.";
                    }
                }
            }

            else if (DrawerIsOut && CanInput)
            {
                my_Text.text = "Press ↑ to operate the drawer(抽屉)";
                if (Input.GetAxis("Talk") == 1)
                {
                    SoundManager.playDrawerClip();
                    DrawerIsOut = false;
                    DrawerCanMove = true;
                    CanInput = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player.GetComponent<PlayerCTRL>().CanRide = true;
        my_Text.text = "";
    }

    void MoveDrawer()
    {
        if (!DrawerIsOut && DrawerCanMove)
        {
            if (moveTimer >= 0)
            {
                transform.position += velocity * Time.deltaTime;
                moveTimer -= Time.deltaTime;
            }
            else
            {
                DrawerCanMove = false;
                CanInput = true;
                moveTimer = moveTime;
            }
        }
        else if (DrawerIsOut && DrawerCanMove)
        {
            if (moveTimer >= 0)
            {
                transform.position -= velocity * Time.deltaTime;
                moveTimer -= Time.deltaTime;
            }
            else
            {
                DrawerCanMove = false;
                CanInput = true;
                moveTimer = moveTime;
            }
        }
    }
}
