using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRoomProgress : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StoryBook1;
    public GameObject StoryBook2;

    public GameObject child1;
    public GameObject child2;

    public GameObject boss;
    
    public GameObject parents;
    public bool canChange = true;
    public bool Hasproblem;
    void Start()
    {
    }
    void Update()
    {
        if (PlayerPrefs.GetFloat("defeat_HWMons", 0f) == 1f && boss != null)
        {
            Destroy(boss);
        }
        
        if (PlayerPrefs.GetFloat("Monster", 0f) == 1f)
        {
            if (child1 != null && child2 != null)
            {
                Destroy(child1);
                Destroy(child2);
            }
        }
        
        Hasproblem = parents.GetComponent<Parent_Give_Problem>().problemExist;
        if (StoryBook1 == null || StoryBook2 == null && canChange)
        {
            parents.GetComponent<Parent_Give_Problem>().problemExist = false;
            canChange = false;
            if (child1 != null)
            {
                Destroy(child1);
                PlayerPrefs.SetFloat("Monster", 1f);
                Debug.Log(PlayerPrefs.GetFloat("Monster"));
            }
        }
        
        if (StoryBook2 == null && StoryBook1 == null)
        {
            if (child2 != null)
            {
                Destroy(child2);
            }
        }

        if (child1 == null && child2 == null)
        {
            if (StoryBook1 != null)
            {
                Destroy(StoryBook1);
            }

            if (StoryBook2 != null)
            {
                Destroy(StoryBook2);
            }
        }
        
        else if ((child1 == null && child2 != null) && StoryBook1 != null && StoryBook2 != null)
        {
            Destroy(StoryBook1);
            PlayerPrefs.SetFloat("Monster", 1f);
            Debug.Log(PlayerPrefs.GetFloat("Monster"));
        }
    }
}
