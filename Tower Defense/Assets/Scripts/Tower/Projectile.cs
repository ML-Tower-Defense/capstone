using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject originalTower;    // Tower that fired this projectile

    private Transform target;           // Target to hit
    private int speed = 7;              // Speed of projectile

    void Start()
    {
        getTargetFromTower();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= 0.2)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame);
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
}
