using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : Weapon
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private bool _hasPulledTrigger; // Serialised for Debugging
    [SerializeField] private float _shootCooldown = 1f;
    [SerializeField] private Transform _aimAssist;

    [Header("Automatic Turret")]
    [SerializeField] private bool _isAutomaticTurret;
    [SerializeField] private bool _autoFire;
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxRange = 100f;
    [SerializeField] private LayerMask _detectionLayer;

    private float oldSootCooldown;
    private bool canShoot;
    private bool hasTarget;
    private bool hasAimAssist;

    private Collider[] targetsInRange;
    private Rigidbody targetRigidbody;
    private Vector3 predictedVelocity;

    public Transform Target => _target;
    public Vector3 PredictedVelocity => predictedVelocity;

    public bool IsAutomaticTurret
    {
        get => _isAutomaticTurret;
        set => _isAutomaticTurret = value;
    }

    private void Start()
    {
        oldSootCooldown = _shootCooldown;
        
        if (_target == null) hasTarget = false;
        
        if (_aimAssist == null) hasAimAssist = false;
    }

    private void DetectTargets()
    {
        targetsInRange = Physics.OverlapSphere(transform.position, _maxRange, _detectionLayer);

        for (int i = 0; i < targetsInRange.Length; i++)
        {
            if (targetsInRange[i].transform == transform.parent)
            {
                continue;
            }
            
            var direction = targetsInRange[i].transform.position - transform.position;
            Physics.Raycast(transform.position, direction, out var hitInfo, _maxRange);

            if (hitInfo.transform == null || hitInfo.transform != targetsInRange[i].transform)
            {
                hasTarget = false;
                continue;
            }

            if (hasTarget)
            {
                Debug.DrawLine(transform.position, _target.transform.position);
                continue;
            }

            _target = targetsInRange[i].transform;
            _target.TryGetComponent(out targetRigidbody);
            hasTarget = true;
        }
    }

    private void ShootCooldown()
    {
        if (canShoot)
        {
            print("Yes");
            return;
        }

        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }
        
        canShoot = _shootCooldown <= 0;
    }

    private void AutoFire()
    {
        if (_shootCooldown <= 0)
        {
            _hasPulledTrigger = true;
        }
    }

    private void FixedUpdate()
    {
        ShootCooldown();

        DetectTargets();
        
        if (!hasTarget) return;

        if (_autoFire)
        {
            AutoFire();
        }

        if (_hasPulledTrigger)
        {
            Fire();
        }

        if (hasAimAssist) return;
        MoveAimAssist();
    }

    private Vector3 Aim(float speed)
    {
        if (targetRigidbody == null && _target != null)
        {
            return _target.position;
        }

        var targetPosition = transform.forward;
        var predictedPosition = targetPosition * 100;
        
        if (_target != null && _isAutomaticTurret)
        {
            targetPosition = _target.position;
            var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            //var predictedPosition = targetPosition + (_targetRigidbody.angularVelocity / _targetRigidbody.angularDrag) + _targetRigidbody.velocity / (speed / (distanceToTarget / speed));
            predictedPosition = targetPosition + targetRigidbody.velocity / (speed / (distanceToTarget / speed));
        }

        return predictedPosition;
    }

    private void MoveAimAssist()
    {
        _aimAssist.position = Aim(_projectilePrefab.MaxSpeed);
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

    public void Fire()
    {
        if (!canShoot) return;
        
        CalculatePredictionVelocity(_projectilePrefab.MaxSpeed);
        var newProjectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        newProjectile.InitBullet(transform.parent, this);
        
        _hasPulledTrigger = false;
        _shootCooldown = oldSootCooldown;
        canShoot = false;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, transform.forward * maxRange);
        Gizmos.DrawWireSphere(transform.position, _maxRange);
    }
}
