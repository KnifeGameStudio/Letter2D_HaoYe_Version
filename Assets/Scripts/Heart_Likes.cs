using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Likes : MonoBehaviour
{
    public bool IsBigOld;
    public Animator anim;
    public SpriteRenderer Sp;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBigOld)
        {
            Sp.enabled = true;
            anim.SetTrigger("Heart_Likes");  
        }
        
        else if (!IsBigOld)
        {
            Sp.enabled = false;
        }
    }
}
