using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform target;
    [SerializeField] private bool fireBullet;
    [SerializeField] private Transform aimAssist;
    [SerializeField] private float speed = 2f;
    
    private Rigidbody _targetRigidbody;
    private Vector3 _predictedVelocity;

    public Transform Target => target;
    public Vector3 PredictedVelocity => _predictedVelocity;

    private void Start()
    {
        _targetRigidbody = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (fireBullet)
        {
            fireBullet = false;
            Fire();
        }

        if (aimAssist == null) return;
        FollowTarget();
    }

    private Vector3 PredictedPosition()
    {
        var targetPosition = target.position;
        var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        var predictedPosition = targetPosition + _targetRigidbody.velocity / (speed / (distanceToTarget / 3f));

        return predictedPosition;
    }

    private void FollowTarget()
    {
        aimAssist.position = PredictedPosition();
    }
    
    public Vector3 GetPredictionVelocity()
    {
        _predictedVelocity = PredictedPosition();
        var velocityDirection = _predictedVelocity - transform.position;

        var velocityMagnitude = velocityDirection.magnitude;

        _predictedVelocity = velocityDirection.normalized;

        _predictedVelocity *= (speed * speed);

        return _predictedVelocity;
    }

    private void Fire()
    {
        GetPredictionVelocity();
        var newProjectile = Instantiate(projectilePrefab);
        newProjectile.SetOrigin(this);
    }
}
