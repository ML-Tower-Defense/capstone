using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        setMaxHealth(1000);
    }

    public void setMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
    }

    public float getHealth()
    {
        return slider.value;
    }

    public void setHealth(float health){
        slider.value = health;
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
