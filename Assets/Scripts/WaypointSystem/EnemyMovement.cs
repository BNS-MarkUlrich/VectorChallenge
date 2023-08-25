using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Waypoint currentWaypoint;
    [SerializeField] private Waypoint lastWaypoint;
    
    private Vector2 moveVelocity;
    private Rigidbody2D myRigidBody;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        if (currentWaypoint == null)
        {
            var hit = Physics2D.OverlapCircle(transform.position, transform.localScale.magnitude);
            hit.TryGetComponent(out currentWaypoint);
            lastWaypoint = currentWaypoint;
        }
        
        MoveToWaypoint();
    }

    private void FixedUpdate()
    {
        if (HasReachedWaypoint())
        {
            UpdateWaypoint();
            MoveToWaypoint();
        }

        Debug.DrawLine(transform.position, currentWaypoint.transform.position);
    }

    private void UpdateWaypoint()
    {
        currentWaypoint.GetConnectedWaypoint(out var newWaypoint, out var isDeadEnd);

        if (isDeadEnd)
        {
            currentWaypoint = lastWaypoint;
        }
        else if (newWaypoint == lastWaypoint)
        {
            UpdateWaypoint();
        }
        else
        {
            lastWaypoint = currentWaypoint;
            currentWaypoint = newWaypoint;
        }
    }

    private void MoveToWaypoint()
    {
        var moveDirection = currentWaypoint.transform.position - transform.position;
        moveVelocity = moveDirection.normalized * (moveSpeed * Time.deltaTime);
        myRigidBody.velocity = moveVelocity;
    }

    private bool HasReachedWaypoint()
    {
        var distance = currentWaypoint.transform.position - transform.position;
        return distance.magnitude <= distanceThreshold;
    }
}