using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //private Transform target;
    private EnemyHealth targetEnemy;

    public float range = 3f;

    public GameObject bulletPrefab; // The projectile the tower shoots
    public float fireRate = 1f;
    public int dmgDealt = 10;

    public string enemyTag = "Enemy";

    public Animator animator; // Tower's animation controller
                                  // Dark Mage animations:
                                  // "2h2", "2h3", "2hand", "area_casting", "area_casting2",
                                  // "casting", "casting2", "continuous_shooting", "continuous_shooting2",
                                  // "dying", "idle", "jump", "run", "shooting", "walk", "walk2"

    public static string idleAnim;   // Idle animation of tower
    public static string attackAnim; // Attack animation of tower


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
                //target = nearestEnemy.transform;

                StartCoroutine(attackEnemy()); // Play attack animation

                targetEnemy = nearestEnemy.GetComponent<EnemyHealth>();   // Enemy to attack
                targetEnemy.TakeDamage(dmgDealt);

                StopCoroutine(attackEnemy());
            }
            else
            {
                //target = null;

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

        yield return new WaitForSecondsRealtime(1);
    }
}
