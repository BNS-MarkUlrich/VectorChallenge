using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : Movement
{
    [SerializeField] private Transform target;

    protected float distanceToTarget;
    
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
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < 2f)
        {
            MyRigidBody.velocity *= 0.5f * Time.deltaTime;
            return;
        }
        
        MoveToTarget(target);
        RotateToTarget(target);
    }
}
