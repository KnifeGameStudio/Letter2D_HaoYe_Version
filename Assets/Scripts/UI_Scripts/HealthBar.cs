using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static int HealthCurrent;
    public static int HealthMax;
    
    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        HealthMax = GameObject.Find("Player").GetComponent<PlayerHealth>().maxi_helath;
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float) HealthCurrent / (float) HealthMax;
    }
}
