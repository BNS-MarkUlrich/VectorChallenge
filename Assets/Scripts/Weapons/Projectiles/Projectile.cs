using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : ZeroGMovement
{
    [SerializeField] protected float maxTravelDistance = 100f;
    [SerializeField] protected float _overheatCost = 10;
    protected Transform ShipOrigin;
    protected Turret Origin;

    public float OverheatCost => _overheatCost;
    public float MaxTravelDistance => maxTravelDistance;
    
    public virtual void InitBullet(Transform shipOrigin, Turret newOrigin)
    {
        ShipOrigin = shipOrigin;
        Origin = newOrigin;
    }

    protected virtual void Launch(Vector3 newVelocity)
    {
        MyRigidBody.velocity = newVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != ShipOrigin && other.transform != Origin.transform)
        {
            TargetHit(other.gameObject);
        }
    }

    protected abstract void TargetHit(GameObject target);
}
