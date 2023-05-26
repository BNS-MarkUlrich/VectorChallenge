using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraMovement : Movement
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private Transform refocusTarget;

    protected override void Awake()
    {
        base.Awake();
        refocusTarget.parent = transform.parent;
    }

    public void MoveRTSCamera(Vector3 input)
    {
        var velocity = input * maxSpeed;
        MyRigidBody.velocity = transform.TransformDirection(velocity);
    }

    public void RotateRTSCamera(Vector3 rotatePoint, Vector2 rotationDelta)
    {
        var rotationVelocity = rotationDelta.x;
        transform.RotateAround(rotatePoint, Vector3.up, rotationVelocity);
    }

    public void ZoomRTSCamera(Vector3 scrollDelta)
    {
        scrollDelta.z = scrollDelta.y;
        scrollDelta.y = 0;
        var velocity = scrollDelta;
        MyRigidBody.velocity += transform.TransformVector(velocity) * zoomSpeed;
    }

    public void FocusOnTarget()
    {
        var refocusPos = refocusTarget.position;
        refocusPos.y = transform.position.y;
        refocusTarget.position = refocusPos;

        MoveToTarget(refocusTarget);
    }
}
