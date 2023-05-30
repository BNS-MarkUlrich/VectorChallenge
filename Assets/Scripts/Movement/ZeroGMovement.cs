using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGMovement : Movement
{
    protected Transform Target;
    private float _distanceToTarget;

    protected bool HasReachedTarget(float distanceThreshold = 1f)
    {
        _distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);

        return _distanceToTarget < distanceThreshold;
    }
    
    protected void MoveToTarget(Transform target)
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
    
    protected void RotateToTarget(Transform rotationTarget)
    {
        var targetDirection = rotationTarget.position - transform.position;
        var angle = Vector3.Angle(targetDirection, transform.forward) / 10;
        var turnDirection = Vector3.Dot(targetDirection, transform.right);
        var pitch = (angle / 100) / Mass;
        if (turnDirection > 0f) 
        {
            pitch = -pitch;
        } 
        transform.Rotate(transform.InverseTransformDirection(transform.forward), pitch);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * (rotationSpeed / angle / Mass));
    }
}
