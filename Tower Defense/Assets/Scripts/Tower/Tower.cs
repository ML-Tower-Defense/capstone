using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;     // The projectile the tower shoots
    public Transform target;

    private EnemyHealth targetEnemy;

    public float range = 3f;
    public float fireRate = 1f;
    public float projectileSpeed = 2f;
    public int dmgDealt = 10;

    public string enemyTag = "Enemy";

    public Animator animator; // Tower's animation controller
                                  // Dark Mage animations:
                                  // "2h2", "2h3", "2hand", "area_casting", "area_casting2",
                                  // "casting", "casting2", "continuous_shooting", "continuous_shooting2",
                                  // "dying", "idle", "jump", "run", "shooting", "walk", "walk2"

    public static string idleAnim;    // Idle animation of tower
    public static string attackAnim;  // Attack animation of tower

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, fireRate);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;

                StartCoroutine(attackEnemy()); // Play attack animation

                StopCoroutine(attackEnemy());
                StopCoroutine(attackEnemy());

                targetEnemy = nearestEnemy.GetComponent<EnemyHealth>();   // Enemy to attack
                targetEnemy.TakeDamage(dmgDealt);
            }
            else
            {
                // Play idle animation if there is
                if (idleAnim != "")
                    animator.Play(idleAnim);
            }
        }
    }

    /*// Update is called once per frame
    void Update()
    {

    }*/

    // Play attack animation
    IEnumerator attackEnemy()
    {
        animator.Play(attackAnim);

        yield return new WaitForSecondsRealtime(0.7f);

        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        yield return new WaitForSecondsRealtime(0.2f);
    }

    public Transform getTarget()
    {
        return target;
    }
}
