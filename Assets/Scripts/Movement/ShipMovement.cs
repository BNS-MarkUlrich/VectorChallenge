using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : Movement
{
    [SerializeField] private bool startMoving;
    
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed = 5f;

    private void Start()
    {
        InitTarget();
    }

    private void InitTarget()
    {
        target.transform.parent = transform.parent;
    }

    public void SetTargetDestination(Vector3 destination)
    {
        target.transform.position = destination;
    }

    private void Update()
    {
        if (!startMoving) return;
        
        var velocityDirection = target.transform.position - transform.position;

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
        
        /*var moveDirection = transform.TransformDirection (Vector3.forward);
        transform.rotation = Quaternion.LookRotation(moveDirection);
        Debug.DrawRay(transform.position, moveDirection);*/
    }
}
