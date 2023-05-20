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
    
    private void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody>();
    }
    
    protected void MoveToTarget(Transform target)
    {
        var velocityDirection = target.position - transform.position;

        /*var velocityMagnitude = velocityDirection.magnitude;

        var desiredVelocity = velocityDirection.normalized * velocityMagnitude;*/
        
        var angle = Vector3.Angle(velocityDirection, transform.forward) / 10;
        
        var currentSpeed = maxSpeed / angle;
        
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        
        MyRigidBody.velocity = transform.forward.normalized * (currentSpeed / Mass);
        //MyRigidBody.velocity = transform.forward.normalized * velocityMagnitude;
    }
    
    protected Quaternion RotateToTarget(Transform rotationTarget)
    {
        var targetDirection = rotationTarget.position - transform.position;
        var angle = Vector3.Angle(targetDirection, transform.forward) / 10;
        return Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * (rotationSpeed / angle / Mass));
    }
}
