using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poem_In_SelfRoom : MonoBehaviour
{
    [Header("与被骑过后消失相关")]
    public SpriteRenderer[] SPs;
    private Color originalColor;
    private Color changeColor;
    void Start()
    {
        originalColor.a = 1f;
        changeColor.a = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        SPs = gameObject.GetComponentsInChildren<SpriteRenderer>();
        IsStoryHere();
    }


    void IsStoryHere()
    {
        if (transform.GetChild(0).CompareTag("Story"))
        {
            foreach (SpriteRenderer sp in SPs)
            {
                sp.color = originalColor;
            }
        }
            
        else
        {
            foreach (SpriteRenderer sp in SPs) 
            {
                sp.color = changeColor;
                StartCoroutine(killSelf());
            }
        }
    }

    IEnumerator killSelf()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
