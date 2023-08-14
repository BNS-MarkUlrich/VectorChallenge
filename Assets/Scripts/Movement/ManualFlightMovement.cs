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
        
        if (thrustVelocity.magnitude <= 0.05)
        {
            thrustVelocity = Vector2.zero;
            turningThrust = thrustVelocity;
            rotationVelocity = turningThrust;
        }

        turningThrust = GetThrust(thrustVelocity, maxPitchThrust);
        rotationVelocity = GetAngularVelocity(turningThrust, maxTurningThrust, maxRotationVelocity);

        if (!ignorePitch)
        {
            pitchThrust = GetThrustAdditive(pitchThrust,thrustVelocity.x, maxPitchThrust);
            pitchVelocity = GetAngularVelocity(pitchThrust, maxPitchThrust, maxPitchVelocity);
            //pitchVelocity = rotationVelocity.x / maxRotationVelocity;
            transform.Rotate(Vector3.forward * -pitchVelocity / Mass);
        }
        else
        {
            ResetPitch();
        }
        
        //MyRigidBody.MoveRotation(Quaternion.Euler(Vector3.right * -rotationVelocity / Mass)); // Todo Mark: Try this out

        transform.Rotate(Vector3.up * rotationVelocity.x / Mass);
        transform.Rotate(Vector3.right * -rotationVelocity.y / Mass);
        
        MyRigidBody.MoveRotation(transform.rotation);
    }
    
    public void ApplyPitchThrust(float thrustMultiplier)
    {
        if (thrustMultiplier == 0 && rotationVelocity.x == 0)
        {
            ResetPitch();
        }
        else
        {
            pitchThrust = GetThrust(thrustMultiplier, maxPitchThrust);
        }

        pitchVelocity = GetAngularVelocity(pitchThrust, maxPitchThrust, maxPitchVelocity);
        
        transform.Rotate(Vector3.forward * -pitchVelocity / Mass);
    }

    public float FixedEulerAngle(float angle)
    {
        if (angle < 180)
        {
            return angle;
        }
        
        var fixedAngle = 360 - angle;
        fixedAngle = -fixedAngle;

        return fixedAngle;
    }

    private static float GetThrust(float thrustMultiplier, float maxThrust)
    {
        var thrust = thrustMultiplier * maxThrust;
        return Mathf.Clamp(thrust, -maxThrust, maxThrust);
    }

    private static Vector2 GetThrust(Vector2 thrustVelocity, float maxThrust)
    {
        var thrust = thrustVelocity * maxThrust;
        
        thrust.x = Mathf.Clamp(thrust.x, -maxThrust, maxThrust);
        thrust.y = Mathf.Clamp(thrust.y, -maxThrust, maxThrust);

        return thrust;
    }
    
    private static float GetThrustAdditive(float thrust, float thrustMultiplier, float maxThrust)
    {
        thrust += thrustMultiplier * maxThrust;
        return Mathf.Clamp(thrust, -maxThrust, maxThrust);
    }
    
    private static float GetThrustFromAngle(float fromAngle, float toAngle, float maxThrust)
    {
        var angle = Mathf.DeltaAngle(fromAngle, toAngle);

        if (fromAngle > 180)
        {
            angle = 360 - angle;
            angle = -angle;;
        }
        
        return angle switch
        {
            >= 1 => GetThrust(angle / 10, maxThrust),
            <= -1 => GetThrust(angle / 10, -maxThrust),
            _ => 0
        };
    }

    private static float GetAngularVelocity(float thrust, float maxThrust, float maxVelocity)
    {
        float velocity = (thrust / maxThrust) * maxVelocity;
        velocity = Mathf.Clamp(velocity, -maxVelocity, maxVelocity);

        return velocity;
    }
    
    private static Vector2 GetAngularVelocity(Vector2 thrust, float maxThrust, float maxVelocity)
    {
        Vector2 velocity = (thrust / maxThrust) * maxVelocity;
        
        velocity.x = Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity);
        velocity.y = Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity);

        return velocity;
    }

    public void RotateTowards(Quaternion targetRotation)
    {
        var rotation = transform.rotation;
        var angle = Quaternion.Angle(targetRotation, rotation);

        turningThrust = GetThrust(Vector2.one * angle, maxTurningThrust);
        rotationVelocity = GetAngularVelocity(turningThrust, maxTurningThrust, maxRotationVelocity);

        transform.rotation = Quaternion.Slerp(rotation, targetRotation, rotationVelocity.magnitude * Time.deltaTime / Mass);
    }
    
    private void RotateToAngle(Vector2 to) // So far hasn't worked, purely experimental
    {
        var eulerAngles = transform.eulerAngles;
        
        turningThrust.x = GetThrustFromAngle(eulerAngles.x, to.x, maxTurningThrust);
        turningThrust.y = GetThrustFromAngle(eulerAngles.y, to.y, maxTurningThrust);
        
        rotationVelocity.x = GetAngularVelocity(turningThrust.x,maxTurningThrust, maxRotationVelocity);
        rotationVelocity.y = GetAngularVelocity(turningThrust.y,maxTurningThrust, maxRotationVelocity);

        transform.Rotate(Vector3.up * rotationVelocity.x / Mass);
        transform.Rotate(Vector3.right * -rotationVelocity.y / Mass);
    }

    private void ResetPitch()
    {
        var eulerAngles = transform.eulerAngles;
        
        pitchThrust = GetThrustFromAngle(eulerAngles.z,0, maxPitchThrust);
        pitchVelocity = GetAngularVelocity(pitchThrust,maxPitchThrust, maxPitchVelocity);

        transform.Rotate(Vector3.forward * -pitchVelocity / Mass);
    }
}
