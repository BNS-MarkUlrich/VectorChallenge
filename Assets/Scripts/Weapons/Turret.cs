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
        MoveAimAssist();
    }

    private Vector3 Aim()
    {
        var targetPosition = target.position;
        var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        var predictedPosition = targetPosition + _targetRigidbody.velocity / (speed / (distanceToTarget / speed));

        return predictedPosition;
    }

    private void MoveAimAssist()
    {
        aimAssist.position = Aim();
    }
    
    public Vector3 CalculatePredictionVelocity()
    {
        _predictedVelocity = Aim();
        var velocityDirection = _predictedVelocity - transform.position;

        //var velocityMagnitude = velocityDirection.magnitude;

        _predictedVelocity = velocityDirection.normalized;

        _predictedVelocity *= (speed * speed);

        return _predictedVelocity;
    }

    private void Fire()
    {
        CalculatePredictionVelocity();
        var newProjectile = Instantiate(projectilePrefab, transform);
        newProjectile.InitBullet(this);
    }
}
