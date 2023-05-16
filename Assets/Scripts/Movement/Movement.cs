using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float turnTimer = 5f;
    private float originalTurnTimer;
    private bool shouldTurn;

    private void Start()
    {
        originalTurnTimer = turnTimer;
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
            rigidbody.velocity = -velocity * speed;
        }
        else
        {
            rigidbody.velocity = velocity * speed;
        }
    }
}
