using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : Movement
{
    [SerializeField] protected Transform target;

    protected float distanceToTarget;

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < 2f)
        {
            MyRigidBody.velocity *= 0.5f * Time.deltaTime;
            return;
        }
        
        MoveToTarget(target);

        RotateToTarget(target);
    }
}
