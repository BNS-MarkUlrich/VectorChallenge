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
    [SerializeField] private Collider2D[] hits = new Collider2D[7];
    [SerializeField] private List<Waypoint> connectedWaypoints = new List<Waypoint>();

    [Header("Debugging")]
    [SerializeField] private bool drawGizmos;

    private Waypoint connectedWaypoint;

    public bool IsOccupied => isOccupied;

    /*private void Awake()
    {
        SetGrid(GetComponentInParent<Grid>());
    }*/

    public void SetGrid(Grid grid)
    {
        parentGrid = grid;
        parentGrid.SubscribeToGrid(this);
    }

    public Waypoint GetConnectedWaypoint()
    {
        RefreshConnectedWaypoints();
        
        var randomIndex = Random.Range(0, connectedWaypoints.Count);
        return connectedWaypoints[randomIndex];
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
                if (connectedWaypoints.Contains(connectedWaypoint))
                {
                    if (connectedWaypoint.isOccupied) connectedWaypoints.Remove(connectedWaypoint);
                    continue;
                }
                
                if (connectedWaypoint.isOccupied) continue;

                connectedWaypoints.Add(connectedWaypoint);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        isOccupied = true;
        parentGrid.UnsubscribeFromGrid(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isOccupied = false;
        parentGrid.SubscribeToGrid(this);
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, connectedWaypointRadius);
        }
    }
}
