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
    // Use 1,2,3,4 to test tower healthbar gradient
    private void Start()
    {
        setMaxHealth(1000);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            setHealth(750);
        }
        if (Input.GetKeyDown("2"))
        {
            setHealth(499);
        }
        if (Input.GetKeyDown("3"))
        {
            setHealth(251);
        }
        if (Input.GetKeyDown("4"))
        {
            setHealth(100);
        }
    }

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
