using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair : MonoBehaviour
{

    public bool is_paired = false;
    public GameObject dui;
    public GameObject x1;
    public GameObject x2;

    public GameObject RidingEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.transform.name == "x1" || collision.transform.name == "x2") && !is_paired)
        {
            {
                SoundManager.playMagicClip();
                Instantiate(RidingEffectPrefab, transform.position,transform.rotation);
                dui.transform.position = x1.transform.position;
                dui.SetActive(true);
                x1.SetActive(false);
                x2.SetActive(false);
                is_paired = true;
            }
        }
    }
}
