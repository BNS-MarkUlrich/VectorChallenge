using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Movement
{
    [SerializeField] protected Turret origin;
    [SerializeField] protected Transform target;
    [SerializeField] protected Rigidbody targetRigidbody;

    public virtual void InitBullet(Turret newOrigin)
    {
        origin = newOrigin;
    }

    protected virtual void Launch(Vector3 newVelocity)
    {
        MyRigidBody.velocity = newVelocity;
    }
}
