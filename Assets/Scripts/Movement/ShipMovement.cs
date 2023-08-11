using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : ZeroGMovement
{
    [SerializeField] private Transform target;
    
    private bool isFollowingTarget;

    private void Start()
    {
        InitTarget();
    }

    private void InitTarget()
    {
        Target = target; 
        target.transform.parent = transform.root.parent;
    }

    public void ResetTarget()
    {
        target.gameObject.SetActive(true);
        isFollowingTarget = true;
        target.transform.position = transform.position;
    }

    public void DisableTarget()
    {
        isFollowingTarget = false;
        target.gameObject.SetActive(false);
    }

    public void SetTargetDestination(Vector3 destination)
    {
        target.transform.position = destination;
    }

    private void Update()
    {
        if (!isFollowingTarget) return;

        if (HasReachedTarget(2f))
        {
            MyRigidBody.velocity *= 0.5f * Time.deltaTime;
            return;
        }
        
        MoveToTarget(target);
        RotateToTarget(target);
    }
}
