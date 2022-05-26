using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int minValue;
    public int value;
    public int maxValue;
    public UnityEngine.UI.Text TextObject = null;
 
    void Start()
    {
        value = 500;
        minValue = 0;
        maxValue = 9999;
        TextObject.text = value.ToString();
    }
    
    public void AddGold(int goldToAdd)
    {
        if ((value + goldToAdd) > maxValue)
        {
            value = maxValue;
            TextObject.text = value.ToString();
        }
        else
        {
            value += goldToAdd;
            TextObject.text = value.ToString();
        }
    }
 
    
    //Function to be implemented on buying tower
    public bool buy(int TowerCost)
    {
        if (TextObject != null)
        {
            if (value >= TowerCost)
            {
                value -= TowerCost;
                TextObject.text = value.ToString();
                return true;
            }
        }
        return false;
    }
   
}
