using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraMovement : Movement
{
    public void MoveRTSCamera(Vector3 input)
    {
        var velocity = input * maxSpeed;
        MyRigidBody.velocity = velocity;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), 0.15f);
    }

    public void RotateRTSCamera(Vector3 rotatePoint, Vector2 rotationDelta)
    {
        var rotationVelocity = rotationDelta.x;
        transform.RotateAround(rotatePoint, Vector3.up, rotationVelocity);
    }

    protected override void MoveToTarget(Transform target)
    {
        
    }

    protected override void RotateToTarget(Transform rotationTarget)
    {
        
    }
}
