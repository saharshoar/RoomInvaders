using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider; 
    
    public void SetMaxHealth(int health)    //function to show the maximum amount of health 
    {
        slider.maxValue = health;
        slider.value = health; 
    }
    public void SetHealth(int health)       //function to set the value of health bar 
    {
        slider.value = health;
    }
}
