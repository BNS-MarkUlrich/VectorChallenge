using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : ZeroGMovement
{
    [SerializeField] private Transform target;
    
    private void Start()
    {
        InitTarget();
    }

    private void InitTarget()
    {
        Target = target;
        target.transform.parent = transform.parent;
    }

    public void SetTargetDestination(Vector3 destination)
    {
        target.transform.position = destination;
    }

    private void Update()
    {
        if (HasReachedTarget(2f))
        {
            MyRigidBody.velocity *= 0.5f * Time.deltaTime;
            return;
        }
        
        MoveToTarget(target);
        RotateToTarget(target);
    }
}
