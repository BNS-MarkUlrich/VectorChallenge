using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGMovement : Movement
{
    protected Transform Target;
    
    protected float DistanceToTarget;
    
    protected bool HasReachedTarget(float distanceThreshold = 1f)
    {
        DistanceToTarget = Vector3.Distance(transform.position, Target.transform.position);

        return DistanceToTarget < distanceThreshold;
    }
    
    protected void MoveToTarget(Transform target, float speed = 0f)
    {
        if (speed == 0f) speed = maxSpeed;

        var velocityDirection = target.position - transform.position;

        var angle = Vector3.Angle(velocityDirection, transform.forward) / 10;
        
        var currentSpeed = speed / angle;
        
        if (currentSpeed >= speed) currentSpeed = speed;

        MyRigidBody.velocity = transform.forward.normalized * (currentSpeed / Mass);
    }
    
    protected void RotateToTarget(Transform rotationTarget, float rotatespeed = 0f)
    {
        if (rotatespeed == 0f) rotatespeed = rotationSpeed;
        
        var targetDirection = rotationTarget.position - transform.position;
        var angle = Vector3.Angle(targetDirection, transform.forward) / 10;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * (rotatespeed / angle / Mass));
    }
}
