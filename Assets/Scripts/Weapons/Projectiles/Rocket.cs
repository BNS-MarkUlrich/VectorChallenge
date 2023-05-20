using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private float areaRadius = 5f;
    [SerializeField] private float lifeTimer = 8f;
    
    private Collider[] targetsInRange;
    private Vector3 followVelocity;
    
    protected float distanceToTarget;

    private void AreaOfEffect()
    {
        Physics.OverlapSphereNonAlloc(transform.position, areaRadius, targetsInRange);
    }

    private void FollowTarget()
    {
        //var velocityDirection = target.position - transform.position;
        
        MoveToTarget(target);
        RotateToTarget(target);
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0 || distanceToTarget < 2f)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void LateUpdate()
    {
        FollowTarget();
        //Launch(followVelocity);
    }

    public override void InitBullet(Turret newOrigin)
    {
        base.InitBullet(newOrigin);
        target = origin.Target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
