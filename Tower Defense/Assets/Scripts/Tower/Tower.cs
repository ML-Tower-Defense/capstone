using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;     // The projectile the tower shoots
    public Transform target;
    AudioManager audioManager;

    private EnemyHealth targetEnemy;

    public float range = 3f;
    public float fireRate = 1f;
    public float projectileSpeed = 2f;
    public int dmgDealt = 10;

    private string enemyTag = "Enemy";

    public Animator animator;  // Tower's animation controller

    public string towerType;   // Specifies a certain tower type
    public string idleAnim;    // Idle animation of tower
    public string attackAnim;  // Attack animation of tower

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        getTowerDetails();
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, fireRate);
    }

    // Get the unique characteristics of a certain tower
    void getTowerDetails()
    {
        switch (towerType)
        {
            case "Mage":
                DarkMageTower darkMageTower = GetComponent<DarkMageTower>();
                idleAnim = darkMageTower.idleAnim;
                attackAnim = darkMageTower.attackAnim;
                break;

            case "Archer":
                ArcherTower archerTower = GetComponent<ArcherTower>();
                idleAnim = archerTower.idleAnim;
                attackAnim = archerTower.attackAnim;
                range = archerTower.range;
                break;

            case "Dragon":
                DragonTower dragonTower = GetComponent<DragonTower>();
                idleAnim = dragonTower.idleAnim;
                attackAnim = dragonTower.attackAnim;
                range = dragonTower.range;
                break;

            default:
                Debug.LogError("Invalid tower type found: " + towerType);
                break;
        }
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

                targetEnemy = nearestEnemy.GetComponent<EnemyHealth>();   // Enemy to attack

                StartCoroutine(attackEnemy()); // Play attack animation

                StopCoroutine(attackEnemy());
                StopCoroutine(attackEnemy());

                //targetEnemy = nearestEnemy.GetComponent<EnemyHealth>();   // Enemy to attack
                //targetEnemy.TakeDamage(dmgDealt);
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
        if (GateManager.gateCurrentHP > 0)
        {
            audioManager.Play("TowerAttack");
        }
        yield return new WaitForSecondsRealtime(0.2f);
    }

    public Transform getTarget()
    {
        return target;
    }

    public EnemyHealth getTargetEnemy()
    {
        return targetEnemy;
    }
}
