using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private float areaRadius = 5f;
    [SerializeField] private float lifeTimer = 8f;
    
    [SerializeField] private Collider[] targetsInRange;
    private Vector3 followVelocity;
    
    protected float distanceToTarget;
    
    public override void InitBullet(Turret newOrigin)
    {
        base.InitBullet(newOrigin);
        target = origin.Target;
    }

    private void AreaOfEffect()
    {
        targetsInRange = Physics.OverlapSphere(transform.position, areaRadius);

        if (targetsInRange.Length == 1)
            return;

        for (int i = 0; i < targetsInRange.Length; i++)
        {
            if (targetsInRange[i].transform == transform)
                continue;
            
            
            print(targetsInRange[i].name);
        }
    }

    private void FollowTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0 || distanceToTarget < 1f)
        {
            AreaOfEffect();
            Destroy(gameObject);
            return;
        }
        
        MoveToTarget(target);
        RotateToTarget(target);
    }

    private void Update()
    {
        FollowTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
