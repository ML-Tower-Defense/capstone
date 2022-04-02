using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyAttack : MonoBehaviour
{
    public GameObject gate;

    bool reachedGate = false;
    bool startedAttacking = false;

    private EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMovement.nextWaypoint == enemyMovement.waypointsArray.Length)
            reachedGate = true;

        if (reachedGate && !startedAttacking)
        {
            StartCoroutine(damageGate(25));
            startedAttacking = true;
        }

        if (GateManager.gateCurrentHP <= 0)
        {
            Destroy(gate);

            if (gameObject.name == "KnightEnemy(Clone)")
            {
                enemyMovement.animator.Play("battlecry");
            }
        }
    }

    IEnumerator damageGate(int damage)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        GateManager.damageGate(damage);

        yield return new WaitForSecondsRealtime(0.4f);

        startedAttacking = false;

        StopAllCoroutines();
    }
}
