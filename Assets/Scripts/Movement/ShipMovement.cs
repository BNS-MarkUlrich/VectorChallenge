using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : ZeroGMovement
{
    [SerializeField] private Transform target;
    [SerializeField] private bool isMovingToTarget;
    
    private void Start()
    {
        InitTarget();
        //InitVelocity();
    }

    private void InitVelocity()
    {
        MyRigidBody.velocity = transform.forward * 0.05f;
    }

    private void InitTarget()
    {
        Target = target;
        target.transform.parent = transform.root.parent;
    }

    public void SetTargetDestination(Vector3 destination)
    {
        target.transform.position = destination;
    }

    private void Update()
    {
        if (!isMovingToTarget) return;
        
        if (HasReachedTarget(2f))
        {
            //InitVelocity();
            MyRigidBody.velocity *= 0.5f * Time.deltaTime;
            return;
        }
        
        MoveToTarget(target);
        RotateToTarget(target);
    }
}
