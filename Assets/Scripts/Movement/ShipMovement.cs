using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : Movement
{
    [SerializeField] protected Transform target;

    private void Update()
    {
        MoveToTarget(target);

        RotateToTarget(target);
    }

    
}
