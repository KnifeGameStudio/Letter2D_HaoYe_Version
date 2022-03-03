using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBook_Trans : MonoBehaviour
{
    public bool OnFound;
    public Transform tranf;
    public Vector3 previousScale;
    
    public AnimationCurve curve;
    [Range(0.1f, 40)] public float durationTime;
    private float x;
    
    void Start()
    {
        previousScale = transform.localScale;
        tranf = GetComponent<Transform>();
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
    }

    void Update()
    {
        Founded();
    }

    void Founded()
    {
        if (OnFound)
        {
            x = Time.deltaTime * 10 / durationTime;
            tranf.localScale = Vector3.Lerp(transform.localScale, previousScale, curve.Evaluate(x));
            Debug.Log(tranf.localScale);
            StartCoroutine(FoundtheStory());
        }
    }

    IEnumerator FoundtheStory()
    {
        yield return new WaitForSeconds(4.5f);
        if (tranf.GetChild(0).name == "StoryBook")
        {
            tranf.GetChild(0).gameObject.SetActive(true);
            tranf.GetChild(0).parent = null;
        }
        Destroy(gameObject);
    }
}
