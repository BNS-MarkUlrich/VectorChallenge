using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private Transform origin;
    [SerializeField] private float lifeTimer = 3f;

    private Vector3 predictedPosition;
    private Vector3 predictionVelocity;

    private void Start()
    {
        var originTarget = origin.GetComponent<Turret>().Target;
        SetTarget(originTarget);
    }

    void LateUpdate()
    {
        /*if (transform.position == predictedPosition)
        {
            Destroy(gameObject);
            return;
        }*/
        
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        Launch();
    }

    public override void SetOrigin(Transform newOrigin)
    {
        origin = newOrigin;
    }

    public override void SetTarget(Transform newTarget)
    {
        target = newTarget;
        targetRigidbody = target.GetComponent<Rigidbody>();
        GetPredictionVelocity();
    }

    public override Vector3 GetPredictionVelocity()
    {
        var targetPosition = target.position;
        predictedPosition = targetPosition + targetRigidbody.velocity / speed;

        var velocityDirection = predictedPosition - transform.position;

        var velocityMagnitude = velocityDirection.magnitude * speed;

        predictionVelocity = velocityDirection.normalized * velocityMagnitude;

        return predictionVelocity;
    }

    public override void Launch()
    {
        print(predictionVelocity);
        rigidbody.velocity = predictionVelocity;
    }
}
