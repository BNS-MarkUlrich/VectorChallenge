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
    [SerializeField] protected Vector3 rotationVelocity;
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
    
    [Header("Pilot Options")]
    [SerializeField] protected bool isBrakingAutomatically;

    /*private void Update()
    {
        if (rotationVelocity == Vector2.zero)
        {
            var rotation = transform.rotation;

            /*rotation.z = Mathf.Lerp(rotation.z, 0, maxTurningThrust * Time.deltaTime / Mass);

            if (rotation.z is < 0.5f and > -0.5f)
            {
                rotation.z = 0;
            }#1#

            rotation.z = 0;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
            
            //transform.Rotate(Vector3.forward, rotation.z);
            //transform.rotation.Set(rotation.x, rotation.y, rotation.z, 1);
            //transform.localRotation = Quaternion.Euler(rotation);
            //print(rotation.z);
        }
    }*/

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
    
    public void ApplyTurningThrust(Vector2 thrustVelocity)
    {
        /*if (thrustVelocity == Vector2.zero) // Mark Note #1: Use for slower maybe more realistic looking rotation, doesn't feel as nice
        {
            turningThrust = Vector2.zero;
            
            rotationVelocity = Vector2.Lerp(rotationVelocity, Vector2.zero,  maxTurningThrust * Time.deltaTime);

            var distanceToZero = rotationVelocity.magnitude;

            if (distanceToZero <= 0.1f)
            {
                rotationVelocity = Vector2.zero;
            }
        }*/

        turningThrust = thrustVelocity * maxTurningThrust;

        turningThrust.x = Mathf.Clamp(turningThrust.x, -maxTurningThrust, maxTurningThrust);
        turningThrust.y = Mathf.Clamp(turningThrust.y, -maxTurningThrust, maxTurningThrust);
        
        rotationVelocity = (turningThrust / maxTurningThrust) * maxRotationVelocity;
        //rotationVelocity += turningThrust / Mass;  // Mark Note #1: Use for slower maybe more realistic looking rotation, doesn't feel as nice
        
        rotationVelocity.x = Mathf.Clamp(rotationVelocity.x, -maxRotationVelocity, maxRotationVelocity);
        rotationVelocity.y = Mathf.Clamp(rotationVelocity.y, -maxRotationVelocity, maxRotationVelocity);
        
        // BEGIN Mark: Auto Pitch Rotation
        /*pitchThrust = thrustVelocity.x * maxPitchThrust;
        pitchThrust = Mathf.Clamp(pitchThrust, -maxPitchThrust, maxPitchThrust);
        rotationVelocity.z = Mathf.Clamp(rotationVelocity.z, -(maxRotationVelocity), (maxRotationVelocity));
        //rotationVelocity.z = rotationVelocity.x / maxRotationVelocity;
        transform.Rotate(Vector3.forward * -rotationVelocity.z / Mass);*/
        // END Mark: Auto Pitch Rotation

        transform.Rotate(Vector3.up * rotationVelocity.x / Mass);
        transform.Rotate(Vector3.right * -rotationVelocity.y / Mass);
    }

    public void ApplyPitchThrust(float thrustMultiplier)
    {
        if (thrustMultiplier == 0 && rotationVelocity.x == 0)
        {
            var eulerAngles = transform.rotation.eulerAngles;

            if (eulerAngles.z > 180)
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

            if (eulerAngles.z is < 0.1f and > -0.1f)
            {
                pitchThrust = 0;
            }
        }
        else
        {
            pitchThrust = thrustMultiplier * maxPitchThrust;
        }
        
        pitchThrust = Mathf.Clamp(pitchThrust, -maxPitchThrust, maxPitchThrust);
        
        rotationVelocity.z = (pitchThrust / maxPitchThrust) * (maxRotationVelocity / 2);
        rotationVelocity.z = Mathf.Clamp(rotationVelocity.z, -(maxRotationVelocity / 2), (maxRotationVelocity / 2));
        
        transform.Rotate(Vector3.forward * -rotationVelocity.z / Mass);
    }
}
