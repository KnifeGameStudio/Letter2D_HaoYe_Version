using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public float Time_To_Destroy;
    void Start()
    {
        Destroy(gameObject,Time_To_Destroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
