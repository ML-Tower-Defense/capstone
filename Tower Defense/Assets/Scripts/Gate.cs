using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public healthBar healthBar;
    public int gateMaxHP = 1000;
    public int gateCurrentHP = 1000;

    void Start(){
        gateCurrentHP = gateMaxHP;
        healthBar.setMaxHealth(gateMaxHP);
    }
    // Update is called once per frame
    void Update(){
        healthBar.setHealth(gateCurrentHP);
    }
    
    public void damageGate(int damage){
        gateCurrentHP -= damage;
    }
}
