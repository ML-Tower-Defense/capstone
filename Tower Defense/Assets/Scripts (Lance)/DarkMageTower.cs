using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMageTower : Tower
{

    // Dark Mage animations:
    // "2h2", "2h3", "2hand", "area_casting", "area_casting2",
    // "casting", "casting2", "continuous_shooting", "continuous_shooting2",
    // "dying", "idle", "jump", "run", "shooting", "walk", "walk2"

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        idleAnim = "idle";
        attackAnim = "2hand";
        animator.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
