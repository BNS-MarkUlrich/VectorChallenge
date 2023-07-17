using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform target;
    [SerializeField] private bool isFiringBullet;
    [SerializeField] private bool autoFire;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private Transform aimAssist;
    [SerializeField] private float maxRange = 100f;
    [SerializeField] private LayerMask detectionLayer;

    private float oldSootCooldown;
    private bool hasTarget;
    private bool hasAimAssist;

    private Collider[] targetsInRange;
    private Rigidbody targetRigidbody;
    private Vector3 predictedVelocity;

    public Transform Target => target;
    public Vector3 PredictedVelocity => predictedVelocity;

    private void Start()
    {
        oldSootCooldown = shootCooldown;
        
        if (target == null) hasTarget = false;
        
        if (aimAssist == null) hasAimAssist = false;
    }

    private void DetectTargets()
    {
        targetsInRange = Physics.OverlapSphere(transform.position, maxRange, detectionLayer);

        for (int i = 0; i < targetsInRange.Length; i++)
        {
            if (targetsInRange[i].transform == transform.parent)
            {
                continue;
            }
            
            var direction = targetsInRange[i].transform.position - transform.position;
            Physics.Raycast(transform.position, direction, out var hitInfo, maxRange);

            if (hitInfo.transform == null || hitInfo.transform != targetsInRange[i].transform)
            {
                hasTarget = false;
                continue;
            }

            if (hasTarget)
            {
                Debug.DrawLine(transform.position, target.transform.position);
                continue;
            }

            target = targetsInRange[i].transform;
            targetRigidbody = target.GetComponent<Rigidbody>(); // Make ComponentCache later
            hasTarget = true;
        }
    }

    private void ShootCooldown()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            shootCooldown = 0f;
        }
    }

    private void AutoFire()
    {
        if (shootCooldown <= 0)
        {
            isFiringBullet = true;
        }
    }

    private void FixedUpdate()
    {
        ShootCooldown();
        
        DetectTargets();
        
        if (!hasTarget) return;

        if (autoFire)
        {
            AutoFire();
        }

        if (isFiringBullet)
        {
            Fire();
        }

        if (hasAimAssist) return;
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
        
        isFiringBullet = false;
        shootCooldown = oldSootCooldown;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, transform.forward * maxRange);
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
