using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : Movement
{
    [SerializeField] private Transform target;

    [SerializeField] private float rotationSpeed = 5f;

    private void Update()
    {
        var velocityDirection = target.position - transform.position;

        var velocityMagnitude = velocityDirection.magnitude;// * (speed / mass);

        var desiredVelocity = velocityDirection.normalized * velocityMagnitude;

        MyRigidBody.velocity = transform.forward.normalized * (speed / mass);
        //MyRigidBody.velocity = transform.forward.normalized * velocityMagnitude;
        
        print(Velocity);
        
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (velocityDirection), Time.deltaTime * (rotationSpeed / mass));
    }
}
