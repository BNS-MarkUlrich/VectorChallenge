using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private float areaRadius = 5f;
    private Collider[] targetsInRange;

    private Vector3 followVelocity;

    private void AreaOfEffect()
    {
        Physics.OverlapSphereNonAlloc(transform.position, areaRadius, targetsInRange);
    }

    private void FollowTarget()
    {
        var velocityDirection = target.position - transform.position;
    }

    private void LateUpdate()
    {
        Launch(followVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
