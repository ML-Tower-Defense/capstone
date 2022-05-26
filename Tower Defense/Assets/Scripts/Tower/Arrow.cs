using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject originalTower;
    public Transform target;
    private EnemyHealth targetHealth;   // Health of target
    private int damage = 10;            // Damage arrow inflicts upon hit

    public float speed = 10f;

    public Vector2 movePosition;

    private float towerX;
    private float targetX;
    private float nextX;
    private float dist;
    private float baseY;
    private float height;
    //private float timeAlive = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        getTargetFromTower();
    }

    // Update is called once per frame
    void Update()
    {
        if (originalTower != null && target != null)
        {
            towerX = originalTower.transform.position.x;
            targetX = target.position.x;

            dist = targetX - towerX;

            nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
            baseY = Mathf.Lerp(originalTower.transform.position.y, target.position.y, (nextX - towerX) / dist);

            height = 2 * (nextX - towerX) * (nextX - targetX) / (-0.25f * dist * dist);

            // print("base: " + baseY + "   height: " + height);

            movePosition = new Vector2(nextX, baseY + height);

            transform.rotation = LookAtTarget(movePosition - (Vector2)transform.position);
            transform.position = movePosition;

            if ((movePosition - (Vector2)target.position).magnitude <= 0.2)
            {
                if (targetHealth.currentHealth > 0)
                {
                    targetHealth.TakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
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
}