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
    [SerializeField] protected float currentRotationSpeed;
    [SerializeField] protected float maxRotationSpeed;
    
    [Header("Forward Thrust")]
    [SerializeField] protected float forwardThrust;
    [SerializeField] protected float maxForwardThrust;
    
    [Header("Backward Thrust")]
    [SerializeField] protected float reverseThrust;
    [SerializeField] protected float maxReverseThrust;
    
    [Header("Turning Thrust")]
    [SerializeField] protected float turningThrust;
    [SerializeField] protected float maxTurningThrust;
    
    [Header("Pilot Options")]
    [SerializeField] protected bool isBrakingAutomatically;

    public void ApplyThrust(float thrustMultiplier)
    {
        if (thrustMultiplier == 0)
        {
            if (isBrakingAutomatically)
            {
                forwardThrust = 0;
                reverseThrust = 0;
            
                var lerp = Mathf.Lerp(currentSpeed, 0,  Time.deltaTime / Mass);
                if (lerp is < 0.5f and > 0 or > -0.5f and < 0)
                {
                    lerp = 0;
                }
                currentSpeed = lerp;
            }
        }
        else if (thrustMultiplier > 0)
        {
            forwardThrust += maxForwardThrust * thrustMultiplier * Time.deltaTime;
            forwardThrust = Mathf.Clamp(forwardThrust, 0, maxForwardThrust);
            currentSpeed += forwardThrust / Mass;
        }
        else if (thrustMultiplier < 0)
        {
            reverseThrust -= maxReverseThrust * thrustMultiplier * Time.deltaTime;
            reverseThrust = Mathf.Clamp(reverseThrust, maxReverseThrust, 0);
            currentSpeed += reverseThrust / Mass;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxSpeed);

        MyRigidBody.velocity = transform.forward.normalized * (currentSpeed * Time.deltaTime);
    }

    public void ApplyLateralThrust(float thrustMultiplier)
    {
        if (thrustMultiplier == 0)
        {
            turningThrust = 0;
            
            var lerp = Mathf.Lerp(currentRotationSpeed, 0,  Time.deltaTime / (Mass / 2));
            if (lerp is < 0.5f and > 0 or > -0.5f and < 0)
            {
                lerp = 0;
            }

            currentRotationSpeed = lerp;
        }
        else if (thrustMultiplier > 0)
        {
            turningThrust += maxTurningThrust * thrustMultiplier * Time.deltaTime;
            turningThrust = Mathf.Clamp(turningThrust, -maxTurningThrust, maxTurningThrust);
            currentRotationSpeed += turningThrust / Mass;
        }
        else if (thrustMultiplier < 0)
        {
            turningThrust -= maxTurningThrust * thrustMultiplier * Time.deltaTime;
            turningThrust = Mathf.Clamp(turningThrust, -maxTurningThrust, maxTurningThrust);
            currentRotationSpeed -= turningThrust / Mass;
        }

        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, -maxRotationSpeed, maxRotationSpeed);
        
        transform.Rotate(Vector3.up * currentRotationSpeed / Mass);
    }
}
