using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Damage of enemy
    public int damage = 20;
    //for attack rate
    float timer, cooldown;
    //for gate object
    public Gate gate;
    public EnemyMovement enemy;

    void Start() {
        cooldown = 2;
        timer = cooldown;
    }
    void Update()
    {
        if (enemy.move == false){
            //if timer is greater than cooldown, take difference in time until attack
            if (timer > 0){
                timer -= Time.deltaTime;
            }
            //if timer reached 0, attack and cooldown
            else if (timer <= 0) {
                gate.damageGate(damage);
                timer = cooldown;
            }
        }
    }
}
