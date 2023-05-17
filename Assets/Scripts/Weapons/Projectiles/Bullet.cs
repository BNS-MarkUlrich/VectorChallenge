using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private Turret origin;

    private Vector3 predictionVelocity;

    void LateUpdate()
    {
        var distanceToOrigin = Vector3.Distance(transform.position, origin.transform.position);
        if (distanceToOrigin >= maxTravelDistance)
        {
            Destroy(gameObject);
            return;
        }
        
        Launch(predictionVelocity);
    }

    public override void SetOrigin(Turret newOrigin)
    {
        origin = newOrigin;
        predictionVelocity = origin.PredictedVelocity;
    }

    protected override void Launch(Vector3 velocity)
    {
        rigidbody.velocity = velocity;
    }
}
