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

    public override void InitBullet(Transform shipOrigin, Turret newOrigin)
    {
        base.InitBullet(shipOrigin, newOrigin);
        Target = Origin.Target;
    }

    protected override void TargetHit(GameObject target)
    {
        AreaOfEffect();
        Destroy(gameObject);
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
        MoveToTarget(Target);
        RotateToTarget(Target);
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0 || HasReachedTarget())
        {
            AreaOfEffect();
            Destroy(gameObject);
            return;
        }
        
        FollowTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
