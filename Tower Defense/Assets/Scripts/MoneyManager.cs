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

    //To test, use up key to increase money and down key to decrease
    void Update()
    {
        if (Input.GetKeyDown("up")) {
            if ((value+300) > maxValue){
                value = maxValue;
                TextObject.text = value.ToString();
            }
            else{
                value += 300;
                TextObject.text = value.ToString();
            }
        }
        if (Input.GetKeyDown("down")) {
            if ((value-300) < minValue){
                value = minValue;
                TextObject.text = value.ToString();
            }
            else{
                value -= 300;
                TextObject.text = value.ToString();
            }
        }
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
 
    /*
       Function to be implemented on buying tower
       public void buy()
       {
           if (TextObject != null)
           {
               --Value;
               TextObject.text = Value.ToString();
           }
       }
   */
}
