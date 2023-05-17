using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected Rigidbody targetRigidbody;
    [SerializeField] protected Rigidbody rigidbody;
    
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float maxTravelDistance = 100f;

    public abstract void SetOrigin(Turret newOrigin);
    protected abstract void Launch(Vector3 velocity);
}
