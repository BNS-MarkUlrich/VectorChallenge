using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : Movement
{
    [SerializeField] private bool startMoving;
    
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 5f;

    private void Update()
    {
        if (!startMoving) return;
        
        var velocityDirection = target.position - transform.position;

        var velocityMagnitude = velocityDirection.magnitude;

        var desiredVelocity = velocityDirection.normalized * velocityMagnitude;
        
        var angle = Vector3.Angle(velocityDirection, transform.forward);

        var newAngle = angle / 10;
        var newSpeed = speed / newAngle;
        
        if (newAngle < 1)
        {
            newSpeed = speed;
        }
        
        MyRigidBody.velocity = transform.forward.normalized * (newSpeed / Mass);
        //MyRigidBody.velocity = transform.forward.normalized * velocityMagnitude;

        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (velocityDirection), Time.deltaTime * (rotationSpeed / newAngle / Mass));
    }
}
