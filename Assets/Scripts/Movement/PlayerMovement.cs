using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public void MovePlayer(Vector3 input)
    {
        var velocity = input * maxSpeed;
        MyRigidBody.velocity = velocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), 0.15f);
    }

    private void Update()
    {
        
        //Debug.DrawRay(transform.position, moveDirection);
    }

    protected override void MoveToTarget(Transform target)
    {
        
    }

    protected override void RotateToTarget(Transform rotationTarget)
    {
        
    }
}
