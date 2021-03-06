using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject originalTower;    // Tower that fired this projectile
    public Animator animator;           // Projectile's animation controller

    private Transform target;           // Target to hit
    private EnemyHealth targetHealth;   // Health of target
    private int speed = 7;              // Speed of projectile
    public int damage;            // Damage projectile inflicts upon hit
    private bool reachedTarget;         // Check if projectile reached target
    //private float timeAlive = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();

        getTargetFromTower();
        //Destroy(gameObject,timeAlive);
    }

    // Update is called once per frame
    void Update()
    {
        if (originalTower != null && target != null)
        {
            Vector3 dir = target.position - transform.position;

            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= 0.2)
            {
                reachedTarget = true;

                StartCoroutine(explode());
            }

            if (!reachedTarget)
            {
                transform.Translate(dir.normalized * distanceThisFrame);
            }
        }

        else
        {
            Destroy(gameObject);
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
        targetHealth = originalTower.GetComponent<Tower>().getTargetEnemy();
    }

    IEnumerator explode()
    {
        animator.Play("Explosion");

        yield return new WaitForSecondsRealtime(0.5f);

        if (targetHealth.currentHealth > 0)
        {
            targetHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
