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

    private HealthBar healthBar;
    public GameObject healthPrefab;

    public int maxHealth = 100;
    public int currentHealth;
    public float range = 3f;
    public float fireRate = 1f;
    public float projectileSpeed = 2f;
    public int dmgDealt = 10;

    private float damageTime = 3f;
    private float dmgInterval = 0f;

    private string enemyTag = "Enemy";

    public Animator animator;               // Tower's animation controller

    public string towerType;                // Specifies a certain tower type
    public string idleAnim;                 // Idle animation of tower
    public string attackAnim;               // Attack animation of tower

    public int killCount = 0;               // Keep track of enemies killed
    public int projectileCount = 0;         // Keep track of projectiles fired

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        getTowerDetails();
        animator = GetComponent<Animator>();
        healthPrefab = Instantiate(healthPrefab);
        healthPrefab.transform.SetParent(gameObject.transform);
        healthPrefab.transform.localPosition = new Vector3(0, 0.5f, 0);
        healthBar = this.GetComponentInChildren(typeof(HealthBar)) as HealthBar;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        InvokeRepeating("UpdateTarget", 0f, fireRate);
    }

    void Update() {
        if (dmgInterval >= damageTime) {
            dmgInterval = 0;
            TakeDamage(1);
        }
        dmgInterval += Time.deltaTime;
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
                range = darkMageTower.range;
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
                projectileCount += 1;
                StopCoroutine(attackEnemy());
                StopCoroutine(attackEnemy());

                if (targetEnemy.currentHealth <= 0)
                {
                    killCount += 1;
                }

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


    public bool TakeDamage(int damage)
    {
        currentHealth -= dmgDealt;
        healthBar.setHealth(currentHealth);

        // Return false if health goes below 0 and tower is destroyed
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return false;
        }

        // Return true if tower still has health
        return true;
    }

    public EnemyHealth getTargetEnemy()
    {
        return targetEnemy;

    }
}
