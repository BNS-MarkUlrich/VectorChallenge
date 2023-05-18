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
    [SerializeField] private bool autoFire;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private Transform aimAssist;
    [SerializeField] private float speed = 2f;

    private float oldSootCooldown;
    
    private Rigidbody _targetRigidbody;
    private Vector3 _predictedVelocity;

    public Transform Target => target;
    public Vector3 PredictedVelocity => _predictedVelocity;

    private void Start()
    {
        _targetRigidbody = target.GetComponent<Rigidbody>();
        oldSootCooldown = shootCooldown;
    }

    private void Update()
    {
        if (autoFire)
        {
            shootCooldown -= Time.deltaTime;

            if (shootCooldown <= 0)
            {
                fireBullet = true;
                shootCooldown = oldSootCooldown;
            }
        }

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
        //var predictedPosition = targetPosition + (_targetRigidbody.angularVelocity / _targetRigidbody.angularDrag) + _targetRigidbody.velocity / (speed / (distanceToTarget / speed));
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
        var newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        newProjectile.InitBullet(this);
    }
}
