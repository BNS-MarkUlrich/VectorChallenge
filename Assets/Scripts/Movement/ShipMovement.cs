using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : Movement
{
    [SerializeField] private bool startMoving;
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed = 5f;
    
    protected Vector3 distanceToTarget;

    private void Update()
    {
        if (isStartingMovement)
        {
            InitialVelocity = MyRigidBody.velocity;
        }
        
        distanceToTarget.x = Vector3.Distance(transform.position, target.transform.position);

        UpperBoundAcceleration.x = MaxThrust / Mass;

        MyRigidBody.velocity = InitialVelocity + UpperBoundAcceleration * Time.deltaTime;
    }

    private void OldUpdate()
    {
        /*if (!startMoving) return;
        
        var velocityDirection = target.position - transform.position;

        var angle = Vector3.Angle(velocityDirection, transform.forward);

        var currentAngle = angle / 10;
        
        var currentSpeed = upperBoundAcceleration / currentAngle;
        
        if (currentAngle < 1)
        {
            currentSpeed = upperBoundAcceleration;
        }

        currentSpeed += upperBoundAcceleration;

        if (currentSpeed >= upperBoundAcceleration)
        {
            currentSpeed = upperBoundAcceleration;
        }

        MyRigidBody.velocity = transform.forward.normalized * currentSpeed / Mass;

        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        
        var finalAcceleration = Mathf.Sqrt(currentSpeed) + 2 * LowerBoundAcceleration * distanceToTarget;
        Mathf.Sqrt(finalAcceleration);
        
        //MyRigidBody.velocity = transform.forward.normalized / finalAcceleration;

        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(velocityDirection), Time.deltaTime * (rotationSpeed / currentAngle / Mass));*/
    }
}
