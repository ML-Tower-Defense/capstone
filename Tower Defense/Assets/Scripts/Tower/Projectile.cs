using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject originalTower;    // Tower that fired this projectile
    public Animator animator;           // Projectile's animation controller

    private Transform target;           // Target to hit
    private int speed = 7;              // Speed of projectile
    private bool reachedTarget;         // Check if projectile reached target
    public bool isDragon;               // Check if dragon's projectile

    void Start()
    {
        if (isDragon)
        {
            animator = GetComponent<Animator>();
        }

        getTargetFromTower();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= 0.2)
        {
            reachedTarget = true;

            if (isDragon)
            {
                StartCoroutine(explode());
            }

            else
            {
                Destroy(gameObject);
            }

        }

        if (!reachedTarget)
        {
            transform.Translate(dir.normalized * distanceThisFrame);
        }
    }

    private void getTargetFromTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject nearestTower = null;

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject tower in towers)
        {
            float distanceToTower = Vector2.Distance(transform.position, tower.transform.position);

            if (distanceToTower < shortestDistance)
            {
                shortestDistance = distanceToTower;
                nearestTower = tower;
            }

            if (nearestTower != null && shortestDistance <= 0.5)
            {
                originalTower = nearestTower;
            }
        }

        target = originalTower.GetComponent<Tower>().getTarget();
    }

    IEnumerator explode()
    {
        animator.Play("Explosion");

        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(gameObject);
    }
}
