using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Grid parentGrid;
    [SerializeField] private float connectedWaypointRadius;
    [SerializeField] private bool isOccupied;
    [SerializeField] private Collider2D[] hits = new Collider2D[8];
    [SerializeField] private List<Waypoint> connectedWaypoints = new List<Waypoint>();

    [Header("Debugging")]
    [SerializeField] private bool drawGizmos;

    private Waypoint connectedWaypoint;
    private bool gameStart;

    private void Awake()
    {
        gameStart = true;
        InitialCollisionCheck();
    }

    private void InitialCollisionCheck()
    {
        var hitCount = Physics2D.OverlapBoxNonAlloc(transform.position, transform.localScale / 2, 0, hits);

        if (hitCount <= 1)
        {
            isOccupied = false;
            return;
        }

        for (int i = 0; i < hitCount; i++)
        {
            if (isOccupied) continue;
            if (hits[i].gameObject == gameObject) continue;
            
            isOccupied = true;
        }
    }

    public void SetGrid(Grid grid)
    {
        parentGrid = grid;
        parentGrid.SubscribeToGrid(this);
    }

    public void GetConnectedWaypoint(out Waypoint newWaypoint, out bool isDeadEnd)
    {
        RefreshConnectedWaypoints();

        isDeadEnd = connectedWaypoints.Count <= 1;

        if (isDeadEnd)
        {
            newWaypoint = null;
            return;
        }
        var randomIndex = Random.Range(0, connectedWaypoints.Count);
        newWaypoint = connectedWaypoints[randomIndex];
    }

    private void RefreshConnectedWaypoints()
    {
        var hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, connectedWaypointRadius, hits);

        if (hitCount < 1) return;

        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].gameObject == gameObject) continue;

            if (hits[i].TryGetComponent(out connectedWaypoint))
            {
                if (connectedWaypoints.Contains(connectedWaypoint) || !hits.Contains(hits[i]))
                {
                    if (connectedWaypoint.isOccupied) connectedWaypoints.Remove(connectedWaypoint);
                    continue;
                }
                
                if (connectedWaypoint.isOccupied) continue;

                connectedWaypoints.Add(connectedWaypoint);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isOccupied) return;
        isOccupied = true;
        parentGrid.UnsubscribeFromGrid(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isOccupied = false;
        parentGrid.SubscribeToGrid(this);
        RefreshConnectedWaypoints();
        InitialCollisionCheck();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        if (!gameStart)
        {
            InitialCollisionCheck();
        }
        
        if (!isOccupied)
        {
            Gizmos.DrawWireCube(transform.position, transform.localScale / 2);
        }

        if (drawGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, connectedWaypointRadius);
        }
    }
}
