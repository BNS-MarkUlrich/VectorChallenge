using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : ZeroGMovement
{
    [SerializeField] protected float maxTravelDistance = 100f;
    protected Turret Origin;
    protected Rigidbody TargetRigidbody;

    public virtual void InitBullet(Turret newOrigin)
    {
        Origin = newOrigin;
    }

    protected virtual void Launch(Vector3 newVelocity)
    {
        MyRigidBody.velocity = newVelocity;
    }
}
