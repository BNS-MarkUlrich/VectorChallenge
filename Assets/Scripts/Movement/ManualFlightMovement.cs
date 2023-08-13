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
    
    [Header("Rotation")]
    [SerializeField] protected Vector2 rotationVelocity;
    [SerializeField] protected float maxRotationVelocity;

    [Header("Forward Thrust")]
    [SerializeField] protected float forwardThrust;
    [SerializeField] protected float maxForwardThrust;
    
    [Header("Backward Thrust")]
    [SerializeField] protected float reverseThrust;
    [SerializeField] protected float maxReverseThrust;
    
    [Header("Turning Thrust")]
    [SerializeField] protected Vector2 turningThrust;
    [SerializeField] protected float maxTurningThrust;
    
    [Header("Pitch Thrust")]
    [SerializeField] protected float pitchThrust;
    [SerializeField] protected float maxPitchThrust;
    [SerializeField] protected float pitchVelocity;
    [SerializeField] protected float maxPitchVelocity;
    
    [Header("Pilot Options")]
    [SerializeField] protected bool isBrakingAutomatically;

    public void ApplyForwardThrust(float thrustMultiplier)
    {
        if (thrustMultiplier == 0)
        {
            forwardThrust = 0;
            reverseThrust = 0;
            
            if (isBrakingAutomatically)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0,  Time.deltaTime / Mass);

                if (currentSpeed is < 0.5f and > -0.5f)
                    currentSpeed = 0;
            }
        }
        else if (thrustMultiplier > 0)
        {
            forwardThrust += thrustMultiplier * maxForwardThrust * Time.deltaTime;
            forwardThrust = Mathf.Clamp(forwardThrust, 0, maxForwardThrust);
            currentSpeed += forwardThrust / Mass;
        }
        else if (thrustMultiplier < 0)
        {
            reverseThrust -= thrustMultiplier * maxReverseThrust * Time.deltaTime;
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
            
            rotationVelocity.x = Mathf.Lerp(rotationVelocity.x, 0,  Time.deltaTime / (Mass / 2));
            if (rotationVelocity.x is < 0.5f and > -0.5f)
                rotationVelocity.x = 0;
        }

        turningThrust.x += maxTurningThrust * thrustMultiplier * Time.deltaTime;
        turningThrust.x = Mathf.Clamp(turningThrust.x, -maxTurningThrust, maxTurningThrust);
        rotationVelocity.x += turningThrust.x / Mass;
        
        rotationVelocity.x = Mathf.Clamp(rotationVelocity.x, -maxRotationVelocity, maxRotationVelocity);
        
        transform.Rotate(Vector3.up * rotationVelocity.x / Mass);
    }
    
    public void ApplyTurningThrust(Vector2 thrustVelocity, bool ignorePitch)
    {
        // BEGIN Mark: Testing some stuff
        /*thrustVelocity.y = -thrustVelocity.x / 2;
        ApplyPitchThrust(thrustVelocity.x);*/
        // END Mark: Testing some stuff
        if (thrustVelocity.magnitude <= 0.05) // Mark Note #1: Use for slower maybe more realistic looking rotation, doesn't feel as nice
        {
            thrustVelocity = Vector2.zero;
            turningThrust = thrustVelocity;
            rotationVelocity = turningThrust;
        }

        turningThrust = thrustVelocity * maxTurningThrust;

        turningThrust.x = Mathf.Clamp(turningThrust.x, -maxTurningThrust, maxTurningThrust);
        turningThrust.y = Mathf.Clamp(turningThrust.y, -maxTurningThrust, maxTurningThrust);
        
        rotationVelocity = (turningThrust / maxTurningThrust) * maxRotationVelocity;
        //rotationVelocity += (Vector3)turningThrust / Mass;  // Mark Note #1: Use for slower maybe more realistic looking rotation, doesn't feel as nice
        
        rotationVelocity.x = Mathf.Clamp(rotationVelocity.x, -maxRotationVelocity, maxRotationVelocity);
        rotationVelocity.y = Mathf.Clamp(rotationVelocity.y, -maxRotationVelocity, maxRotationVelocity);

        if (!ignorePitch)
        {
            // BEGIN Mark: Auto Pitch Rotation
            pitchThrust += thrustVelocity.x * maxPitchThrust;
            pitchThrust = Mathf.Clamp(pitchThrust, -maxPitchThrust, maxPitchThrust);
            pitchVelocity = (pitchThrust / maxPitchThrust) * maxPitchVelocity;
            pitchVelocity = Mathf.Clamp(pitchVelocity, -(maxPitchVelocity), (maxPitchVelocity));
            //pitchVelocity = rotationVelocity.x / maxRotationVelocity;
            transform.Rotate(Vector3.forward * -pitchVelocity / Mass);
            // END Mark: Auto Pitch Rotation
        }
        else
        {
            ResetPitch();
        }

        transform.Rotate(Vector3.up * rotationVelocity.x / Mass);
        transform.Rotate(Vector3.right * -rotationVelocity.y / Mass);
    }

    private void ResetPitch()
    {
        var eulerAngles = transform.rotation.eulerAngles;
        
        var isInSafeZone = eulerAngles.z is < 1f and > -1f;
        if (isInSafeZone)
        {
            pitchThrust = 0;
        }
        else if (eulerAngles.z > 180)
        {
            var fixedAngle = 360 - eulerAngles.z;
            fixedAngle = -fixedAngle;
            eulerAngles.z = fixedAngle;

            pitchThrust = -maxPitchThrust;
        }
        else if (eulerAngles.z is > 0 and < 180)
        {
            pitchThrust = maxPitchThrust;
        }

        pitchThrust = Mathf.Clamp(pitchThrust, -maxPitchThrust, maxPitchThrust);
        
        pitchVelocity = (pitchThrust / maxPitchThrust) * maxPitchVelocity;
        pitchVelocity = Mathf.Clamp(pitchVelocity, -maxPitchVelocity, maxPitchVelocity);
        
        transform.Rotate(Vector3.forward * -pitchVelocity / Mass);
    }

    public void ApplyPitchThrust(float thrustMultiplier)
    {
        if (thrustMultiplier == 0 && rotationVelocity.x == 0)
        {
            ResetPitch();
        }
        else
        {
            pitchThrust = thrustMultiplier * maxPitchThrust;
        }
        
        pitchThrust = Mathf.Clamp(pitchThrust, -maxPitchThrust, maxPitchThrust);
        
        pitchVelocity = (pitchThrust / maxPitchThrust) * maxPitchVelocity;
        pitchVelocity = Mathf.Clamp(pitchVelocity, -maxPitchVelocity, maxPitchVelocity);
        
        transform.Rotate(Vector3.forward * -pitchVelocity / Mass);
    }
}
