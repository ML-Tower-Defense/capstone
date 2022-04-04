using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private int speed = 2;

    // Update is called once per frame
    void Update()
    {
        // TODO: Make projectile go towards target

        transform.Translate(Vector3.right * Time.deltaTime * speed);

        //target = tower.getTarget();

        //if (target == null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //Vector3 dir = target.position - transform.position;
        //float distanceThisFrame = speed * Time.deltaTime;

        //if (dir.magnitude <= distanceThisFrame)
        //{
        //    HitTarget();
        //    return;
        //}

        //transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Destroy(gameObject);
        Debug.Log("Target hit!");
    }
}
