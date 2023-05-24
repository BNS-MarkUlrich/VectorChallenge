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
    
    protected override void MoveToTarget(Transform target)
    {
        var velocityDirection = target.position - transform.position;

        var angle = Vector3.Angle(velocityDirection, transform.forward) / 10;
        
        var currentSpeed = maxSpeed / angle;
        
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        
        MyRigidBody.velocity = transform.forward.normalized * (currentSpeed / Mass);
    }
    
    protected override void RotateToTarget(Transform rotationTarget)
    {
        var targetDirection = rotationTarget.position - transform.position;
        var angle = Vector3.Angle(targetDirection, transform.forward) / 10;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * (rotationSpeed / angle / Mass));
    }
}
