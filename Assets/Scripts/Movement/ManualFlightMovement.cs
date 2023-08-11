using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ManualFlightMovement : Movement
{
    [Header("Speed")]
    [SerializeField] protected float currentSpeed;
    [SerializeField] protected float maxReverseSpeed;
    
    [Header("Forward Thrust")]
    [SerializeField] protected float forwardThrust;
    [SerializeField] protected float maxForwardThrust;
    
    [Header("Backward Thrust")]
    [SerializeField] protected float backwardThrust;
    [SerializeField] protected float maxBackwardThrust;
    
    [Header("Turning Thrust")]
    [SerializeField] protected float turningThrust;
    [SerializeField] protected float maxTurningThrust;
    
    [Header("Pilot Options")]
    [SerializeField] protected bool isLockingSpeed;

    public void ApplyThrust(float thrustMultiplier)
    {
        if (thrustMultiplier == 0)
        {
            if (isLockingSpeed) return;
            
            var lerp = Mathf.Lerp(currentSpeed, 0,  0.5f * Time.deltaTime);
            if (lerp is < 0.5f and > 0 or > -0.5f and < 0)
            {
                lerp = 0;
            }
            currentSpeed = lerp;
        }

        if (thrustMultiplier > 0)
        {
            backwardThrust = 0;
            forwardThrust = maxForwardThrust * thrustMultiplier * Time.deltaTime;
            currentSpeed += forwardThrust;
        }
        else
        {
            forwardThrust = 0;
            backwardThrust = maxBackwardThrust * thrustMultiplier * Time.deltaTime;
            currentSpeed -= backwardThrust;
        }
        
        currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxSpeed);

        MyRigidBody.velocity = transform.forward.normalized * (currentSpeed * Time.deltaTime);
    }

    public void ApplyTurningThrust()
    {
        
    }
}
