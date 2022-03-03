using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleHurry : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RidingEffectPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Double_Ear"))
        {
            Instantiate(RidingEffectPrefab, transform.position, transform.rotation);
            SoundManager.playMagicClip();
            other.tag = "Invisible";
            Destroy(other.transform.GetChild(0).GetChild(0).gameObject);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            other.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log(other.name);
            transform.position = other.transform.position;
            gameObject.transform.parent = other.transform.GetChild(0);
        }
    }
}
