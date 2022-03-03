using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiang : MonoBehaviour
{
    public bool paring;

    public GameObject x1;
        
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        paring = x1.GetComponent<Pair>().is_paired;
    }
}
