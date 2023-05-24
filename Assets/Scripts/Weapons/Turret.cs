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

    private float oldSootCooldown;
    
    private Rigidbody targetRigidbody;
    private Vector3 predictedVelocity;

    public Transform Target => target;
    public Vector3 PredictedVelocity => predictedVelocity;

    private void Start()
    {
        targetRigidbody = target.GetComponent<Rigidbody>();
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

    private Vector3 Aim(float speed)
    {
        if (targetRigidbody == null)
        {
            return target.position;
        }
        
        var targetPosition = target.position;
        var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        //var predictedPosition = targetPosition + (_targetRigidbody.angularVelocity / _targetRigidbody.angularDrag) + _targetRigidbody.velocity / (speed / (distanceToTarget / speed));
        var predictedPosition = targetPosition + targetRigidbody.velocity / (speed / (distanceToTarget / speed));
        
        return predictedPosition;
    }

    private void MoveAimAssist()
    {
        aimAssist.position = Aim(projectilePrefab.MaxSpeed);
    }
    
    public Vector3 CalculatePredictionVelocity(float speed)
    {
        predictedVelocity = Aim(speed);
        var velocityDirection = predictedVelocity - transform.position;

        //var velocityMagnitude = velocityDirection.magnitude;

        predictedVelocity = velocityDirection.normalized;

        predictedVelocity *= speed;

        return predictedVelocity;
    }

    private void Fire()
    {
        CalculatePredictionVelocity(projectilePrefab.MaxSpeed);
        var newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        newProjectile.InitBullet(this);
    }
}
