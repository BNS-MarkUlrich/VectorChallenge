using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform target;
    [SerializeField] private bool fireBullet;

    public Transform Target => target;

    void Update()
    {
        if (fireBullet)
        {
            fireBullet = false;
            Fire(projectilePrefab);
        }
    }

    private void Fire(Projectile projectile)
    {
        Instantiate(projectile);
        projectile.SetOrigin(transform);

        //projectile.SetTarget(target);
        //projectile.GetPredictionVelocity();
        //projectile.Launch();
    }
}
