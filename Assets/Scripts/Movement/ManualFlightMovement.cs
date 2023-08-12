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
    [SerializeField] protected Vector2 currentRotationVelocity;
    [SerializeField] protected float maxRotationSpeed;
    
    [Header("Forward Thrust")]
    [SerializeField] protected float forwardThrust;
    [SerializeField] protected float maxForwardThrust;
    
    [Header("Backward Thrust")]
    [SerializeField] protected float reverseThrust;
    [SerializeField] protected float maxReverseThrust;
    
    [Header("Turning Thrust")]
    [SerializeField] protected Vector2 turningThrust;
    [SerializeField] protected float maxTurningThrust;
    
    [Header("Pilot Options")]
    [SerializeField] protected bool isBrakingAutomatically;

    public void ApplyForwardThrust(float thrustMultiplier)
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
            turningThrust.x = 0;
            
            var lerp = Mathf.Lerp(currentRotationVelocity.x, 0,  Time.deltaTime / (Mass / 2));
            if (lerp is < 0.5f and > 0 or > -0.5f and < 0)
            {
                lerp = 0;
            }

            currentRotationVelocity.x = lerp;
        }

        turningThrust.x += maxTurningThrust * thrustMultiplier * Time.deltaTime;
        turningThrust.x = Mathf.Clamp(turningThrust.x, -maxTurningThrust, maxTurningThrust);
        currentRotationVelocity.x += turningThrust.x / Mass;
        
        currentRotationVelocity.x = Mathf.Clamp(currentRotationVelocity.x, -maxRotationSpeed, maxRotationSpeed);
        
        transform.Rotate(Vector3.up * currentRotationVelocity.x / Mass);
    }
    
    public void ApplyTurningThrust(Vector2 thrustVelocity)
    {
        if (thrustVelocity == Vector2.zero)
        {
            turningThrust = Vector2.zero;
            
            var lerpV = Vector2.Lerp(currentRotationVelocity, Vector2.zero,  maxTurningThrust * Time.deltaTime);

            var distanceToZero = currentRotationVelocity.magnitude;

            if (distanceToZero <= 0.1f)
            {
                lerpV = Vector2.zero;
            }
            
            currentRotationVelocity = lerpV;
        }

        turningThrust = thrustVelocity * maxTurningThrust;

        turningThrust.x = Mathf.Clamp(turningThrust.x, -maxTurningThrust, maxTurningThrust);
        turningThrust.y = Mathf.Clamp(turningThrust.y, -maxTurningThrust, maxTurningThrust);

        currentRotationVelocity = (turningThrust / maxTurningThrust) * maxRotationSpeed;
        
        currentRotationVelocity.x = Mathf.Clamp(currentRotationVelocity.x, -maxRotationSpeed, maxRotationSpeed);
        currentRotationVelocity.y = Mathf.Clamp(currentRotationVelocity.y, -maxRotationSpeed, maxRotationSpeed);

        transform.Rotate(Vector3.up * currentRotationVelocity.x / Mass);
        transform.Rotate(Vector3.right * -currentRotationVelocity.y / Mass);
    }
}
