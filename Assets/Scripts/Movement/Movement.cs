using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float maxSpeed = 20f;
    [SerializeField] protected float rotationSpeed = 5f;
    
    protected Rigidbody MyRigidBody;
    protected Vector3 Velocity => MyRigidBody.velocity;
    protected float Mass => MyRigidBody.mass;

    public float MaxSpeed => maxSpeed;
    
    protected virtual void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody>();
    }
    
    protected virtual void MoveToTarget(Transform target)
    {
        var velocityDirection = target.position - transform.position;

        var angle = Vector3.Angle(velocityDirection, transform.forward) / 10;
        
        var currentSpeed = maxSpeed / angle;
        
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        
        MyRigidBody.velocity = transform.forward.normalized * (currentSpeed / Mass);
    }
    
    protected virtual void RotateToTarget(Transform rotationTarget)
    {
        var targetDirection = rotationTarget.position - transform.position;
        var angle = Vector3.Angle(targetDirection, transform.forward) / 10;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * (rotationSpeed / angle / Mass));
    }
}
