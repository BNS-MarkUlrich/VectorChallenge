using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : Weapon
{
    [SerializeField] private Transform barrel;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _shootCooldownTimer = 1f;
    [SerializeField] private Transform _aimAssist;
    [SerializeField] private float _heatCapacity;
    [SerializeField] private float _maxHeatCapacity = 100f;
    [SerializeField] private float _overheatTimer = 5f;
    [SerializeField] private float _cooldownStartTimer = 0.5f;

    [Header("Automatic Turret")]
    [SerializeField] private bool _isAutomaticTurret;
    [SerializeField] private Transform _target;
    [SerializeField] private float _detectionRange = 100f;
    [SerializeField] private LayerMask _detectionLayer;

    private float maxCooldownStartTimer;
    private float maxShootCooldownTimer;
    private bool canShoot;
    private bool hasTarget;
    private bool hasAimAssist;
    private bool hasPulledTrigger;
    private bool isBeingUsed;

    private Collider[] targetsInRange;
    private Rigidbody targetRigidbody;
    private Vector3 predictedVelocity;

    public Transform AimAssist => _aimAssist.transform;
    public Transform Target => _target;
    public Vector3 PredictedVelocity => predictedVelocity;

    public bool IsAutomaticTurret
    {
        get => _isAutomaticTurret;
        set => _isAutomaticTurret = value;
    }

    public bool IsBeingUsed
    {
        get => isBeingUsed;
        set => isBeingUsed = value;
    }

    private void Start()
    {
        maxCooldownStartTimer = _cooldownStartTimer;
        maxShootCooldownTimer = _shootCooldownTimer;
        
        if (_target == null) hasTarget = false;
        
        if (_aimAssist == null) hasAimAssist = false;

        if (barrel == null) barrel = transform;
    }

    private void DetectTargets()
    {
        targetsInRange = Physics.OverlapSphere(transform.position, _detectionRange, _detectionLayer);

        // Todo: Add better detection for friendly targets

        if (targetsInRange.Length == 0) return;

        var targetIsParent = targetsInRange[0].transform == transform.parent;
        if (targetsInRange.Length == 1 && targetIsParent) return;

        for (int i = 0; i < targetsInRange.Length; i++)
        {
            if (targetsInRange[i].transform == transform.parent)
            {
                continue;
            }
            
            var direction = targetsInRange[i].transform.position - transform.position;
            Physics.Raycast(transform.position, direction, out var hitInfo, _detectionRange);

            if (hitInfo.transform == null || hitInfo.collider.gameObject.layer != 6) // Removed, caused hasTarget to constantly be set to false. Unknown why added in the first place: || hitInfo.transform != targetsInRange[i].transform
            {
                hasTarget = false;
                hasAimAssist = false;
                _aimAssist.gameObject.SetActive(false);
                continue;
            }

            if (hasTarget)
            {
                if (isBeingUsed) _aimAssist.gameObject.SetActive(true);
                Debug.DrawLine(transform.position, _target.transform.position);
                continue;
            }

            _target = targetsInRange[i].transform;
            _target.TryGetComponent(out targetRigidbody);
            hasTarget = true;
        }
    }

    private void Cooldown()
    {
        _cooldownStartTimer -= Time.deltaTime;
        if (_cooldownStartTimer > 0) return;
        
        _cooldownStartTimer = 0;
        
        if (_heatCapacity < 1)
        {
            _heatCapacity = 0;
            return;
        }

        var lerp = Mathf.Lerp(_heatCapacity, 0, Time.deltaTime);
        _heatCapacity = lerp;
    }

    private void ShootCooldown()
    {
        if (_shootCooldownTimer <= 0) return;

        _shootCooldownTimer -= Time.deltaTime;

        canShoot = _shootCooldownTimer <= 0;

        if (canShoot) _shootCooldownTimer = 0;
    }

    private void AutoFire()
    {
        if (canShoot)
        {
            hasPulledTrigger = true;
        }
    }

    private void FixedUpdate()
    {
        Cooldown();

        ShootCooldown();

        DetectTargets();

        if (!hasAimAssist) _aimAssist.localPosition = -barrel.forward;
        
        if (_isAutomaticTurret && !hasTarget) return;

        if (hasTarget) MoveAimAssist();

        if (_isAutomaticTurret) AutoFire();

        if (hasPulledTrigger) Fire();
    }

    private Vector3 ManualAim(float speed)
    {
        var targetPosition = barrel.transform.forward;
        var aimPosition = targetPosition * (_projectilePrefab.MaxTravelDistance * 10); // Todo: improve manual aim

        return aimPosition;
    }

    private Vector3 AssistedAim(float speed)
    {
        // Todo: Add Aim Assist/Lock on feature?
        
        if (targetRigidbody == null)
            return _target.position;

        var targetPosition = _target.position;
        var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        var predictedPosition = targetPosition + targetRigidbody.velocity / (speed / (distanceToTarget / speed));

        return predictedPosition;
    }

    private void MoveAimAssist()
    {
        _aimAssist.position = AssistedAim(_projectilePrefab.MaxSpeed);
    }
    
    public Vector3 CalculateVelocity(float speed)
    {
        predictedVelocity = _isAutomaticTurret ? AssistedAim(speed) : ManualAim(speed);
        
        var velocityDirection = predictedVelocity - transform.position;
        predictedVelocity = velocityDirection.normalized;
        predictedVelocity *= speed * speed;

        return predictedVelocity;
    }

    public void Fire()
    {
        if (!canShoot) return;
        
        CalculateVelocity(_projectilePrefab.MaxSpeed);
        var newProjectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        newProjectile.InitBullet(transform.parent, this);

        AddHeat(newProjectile.OverheatCost);
        
        TurretReset();
    }

    private void AddHeat(float heat)
    {
        // Todo: Possibly add check whether adding heat would exceed max capacity, block action if so
        
        _heatCapacity += heat;
        _cooldownStartTimer = maxCooldownStartTimer;
        
        if (_heatCapacity >= _maxHeatCapacity)
        {
            // Todo: Add percentile debuff based on how much the heat capacity does/would exceed the max capacity
            
            canShoot = false;
            _shootCooldownTimer += _overheatTimer;
        }
    }

    private void TurretReset()
    {
        hasPulledTrigger = false;
        _shootCooldownTimer += maxShootCooldownTimer;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, transform.forward * maxRange);
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}
