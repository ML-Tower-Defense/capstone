using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTower : Tower
{
    // Dragon animations:
    // "attack", "crouch", "crouch_attack",
    // "idle", "jump_attack"

    void Awake()
    {
        animator = GetComponent<Animator>();
        idleAnim = "idle";
        attackAnim = "attack";
        animator.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
