using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
     public Slider slider;
     public Gradient gradient;
     public Image fill;

    // Update not needed, drag respective healthbar aspect 
    // on object

    public void setMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public float getHealth()
    {
        return slider.value;
    }

    public void setHealth(float health){
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void increaseHealth(float health)
    {
        slider.value += health;
    }

    public void decreaseHealth(float health)
    {
        slider.value -= health;
    }

}
