using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovement : Movement
{
    [SerializeField] protected float turnTimer = 5f;
    private float originalTurnTimer;
    private bool shouldTurn;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        originalTurnTimer = turnTimer;
        
        MoveForward(shouldTurn);
    }

    private void Update()
    {
        turnTimer -= Time.deltaTime;
        if (turnTimer <= 0)
        {
            shouldTurn = !shouldTurn;
            MoveForward(shouldTurn);
            turnTimer = originalTurnTimer;
        }
    }

    private void MoveForward(bool turn)
    {
        if (turn)
        {
            Rigidbody.velocity = -velocity * speed;
        }
        else
        {
            Rigidbody.velocity = velocity * speed;
        }
    }
}
