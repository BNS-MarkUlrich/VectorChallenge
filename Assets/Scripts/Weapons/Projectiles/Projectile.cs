using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Movement
{
    [SerializeField] protected Transform target;
    [SerializeField] protected Rigidbody targetRigidbody;
    
    [SerializeField] protected float maxTravelDistance = 100f;

    public abstract void InitBullet(Turret newOrigin);
    protected abstract void Launch(Vector3 newVelocity);
}
