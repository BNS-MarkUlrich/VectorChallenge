using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RTSCameraMovement : ZeroGMovement
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float refocusSpeedModifier = 3f;
    [SerializeField] private Transform refocusParent;
    [SerializeField] private Transform refocusTarget;
    [SerializeField] private Transform rtsCamera;

    [SerializeField] private UnityEvent onReachedTarget = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();
        InitRefocusTarget();
    }

    private void InitRefocusTarget()
    {
        if (refocusParent == null)
        {
            refocusParent = transform.parent;
        }
        refocusTarget.parent = refocusParent;
        refocusTarget.localPosition = Vector3.zero;
        
        Target = refocusTarget;
    }

    public void MoveRTSCamera(Vector3 input)
    {
        var velocity = input * maxSpeed;
        MyRigidBody.velocity = rtsCamera.transform.TransformDirection(velocity);
    }

    public void RotateRTSCamera(Vector2 rotationDelta)
    {
        var rotationVelocity = rotationDelta.x;
        rtsCamera.transform.RotateAround(transform.position, Vector3.up, rotationVelocity);
    }

    public void ZoomRTSCamera(Vector3 scrollDelta)
    {
        scrollDelta.z = scrollDelta.y;
        scrollDelta.y = 0;
        var velocity = scrollDelta;
        rtsCamera.transform.position += rtsCamera.transform.TransformVector(velocity) * zoomSpeed;
    }

    public void FocusOnTarget()
    {
        if (HasReachedTarget())
        {
            onReachedTarget?.Invoke();
            return;
        }
        
        MoveToTarget(refocusTarget, maxSpeed * refocusSpeedModifier);
        RotateToTarget(refocusTarget, rotationSpeed * refocusSpeedModifier);
    }
}
