using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    // Archer animations:
    // "charging", "dying", "high_shoot", "idle", "jump",
    // "run", "shoot", "shoot2", "walking", "walking_charging"

    void Awake()
    {
        animator = GetComponent<Animator>();
        idleAnim = "idle";
        attackAnim = "shoot";
        animator.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
