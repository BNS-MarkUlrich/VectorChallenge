using System;
using UnityEngine;

public class Turret : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform target;
    [SerializeField] private bool fireBullet;
    [SerializeField] private bool autoFire;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private Transform aimAssist;
    [SerializeField] private float maxRange = 100f;
    [SerializeField] private LayerMask detectionLayer;

    private float oldSootCooldown;
    private bool hasTarget;

    private Collider[] targetsInRange;
    private Rigidbody targetRigidbody;
    private Vector3 predictedVelocity;

    public Transform Target => target;
    public Vector3 PredictedVelocity => predictedVelocity;

    private void Start()
    {
        oldSootCooldown = shootCooldown;
        
        if (target == null) hasTarget = false;
    }

    private void DetectTargets()
    {
        targetsInRange = Physics.OverlapSphere(transform.position, maxRange, detectionLayer);

        /*if (targetsInRange.Length == 1)
        {
            hasTarget = false;
            return;
        }*/

        if (hasTarget) return;

        for (int i = 0; i < targetsInRange.Length; i++)
        {
            Physics.Linecast(transform.position, targetsInRange[i].transform.position, out var hitInfo);

            if (hitInfo.transform != targetsInRange[i].transform)
            {
                continue;
            }
            
            Debug.DrawLine(transform.position, targetsInRange[i].transform.position);
            
            target = targetsInRange[i].transform;
            targetRigidbody = target.GetComponent<Rigidbody>(); // Make ComponentCache later
            hasTarget = true;

            print(targetsInRange[i].name);
        }
    }

    private void FixedUpdate()
    { 
        DetectTargets();
        
        if (!hasTarget) return;

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

        predictedVelocity *= speed * speed;

        return predictedVelocity;
    }

    private void Fire()
    {
        CalculatePredictionVelocity(projectilePrefab.MaxSpeed);
        var newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        newProjectile.InitBullet(transform.parent, this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * maxRange);
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
