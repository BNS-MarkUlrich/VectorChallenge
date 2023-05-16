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

    public abstract void SetOrigin(Transform newOrigin);
    public abstract void SetTarget(Transform newTarget);
    public abstract Vector3 GetPredictionVelocity();
    public abstract void Launch();

}
