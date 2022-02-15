using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public HealthBar healthBar;
    public int gateMaxHP = 1000;
    public static int gateCurrentHP;

    void Start(){
        gateCurrentHP = gateMaxHP;
        healthBar.setMaxHealth(gateMaxHP);
    }
    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown("space")) {
            damageGate(100);
        }
        healthBar.setHealth(gateCurrentHP);
    }
    
    public static void damageGate(int damage){
        gateCurrentHP -= damage;
    }
}
