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

    [Header("Performance")]
    [SerializeField] private int maxWaypointCallStack = 2;
    
    private int newWaypointCallStack;
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
            GetNewWaypoint();
            MoveToWaypoint();
        }

        Debug.DrawLine(transform.position, currentWaypoint.transform.position);
    }

    private void GetNewWaypoint()
    {
        if (newWaypointCallStack >= maxWaypointCallStack)
        {
            currentWaypoint = lastWaypoint;
            newWaypointCallStack = 0;
        }
        
        if (currentWaypoint.GetConnectedWaypoint() != lastWaypoint)
        {
            lastWaypoint = currentWaypoint;
            currentWaypoint = currentWaypoint.GetConnectedWaypoint();
            newWaypointCallStack = 0;
        }
        else
        {
            currentWaypoint.GetConnectedWaypoint();
            newWaypointCallStack++;
            print($"{transform.name} : {newWaypointCallStack}");
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
