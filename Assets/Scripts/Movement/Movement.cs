using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float maxSpeed = 20;

    [SerializeField] protected float MaxThrust = 20;
    protected Vector3 UpperBoundAcceleration;
    
    protected float ReverseThrust => -MaxThrust / 2;
    protected Vector3 LowerBoundAcceleration => -UpperBoundAcceleration / 2;

    protected bool isStartingMovement;
    protected Vector3 InitialVelocity;

    protected Rigidbody MyRigidBody;
    protected Vector3 Velocity => MyRigidBody.velocity;
    protected float Mass => MyRigidBody.mass;
    
    private void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody>();
    }
}
