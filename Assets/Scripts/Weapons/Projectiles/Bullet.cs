using System;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector3 launchVelocity;

    private void LateUpdate()
    {
        var travelDistance = Vector3.Distance(transform.position, Origin.transform.position);
        if (travelDistance >= maxTravelDistance)
        {
            Destroy(gameObject);
            return;
        }
        
        Launch(launchVelocity);
    }

    public override void InitBullet(Transform shipOrigin, Turret newOrigin)
    {
        base.InitBullet(shipOrigin, newOrigin);
        launchVelocity = Origin.PredictedVelocity;
        Target = Origin.Target;
    }

    protected override void TargetHit(GameObject target)
    {
        Destroy(gameObject);
        print(target.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Todo: Add Hit Event
        TargetHit(collision.gameObject);
    }

}
