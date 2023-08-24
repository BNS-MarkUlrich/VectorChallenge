using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    [SerializeField] private Waypoint waypointPrefab;
    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();
    
    [Header("Grid Creation")]
    [SerializeField] private Vector2 GridSize;
    [SerializeField] private bool updateGrid;
    
    private Vector2 initPosition;
    private Vector2 safeZone;
    private Vector2 waypointAmount;
    private bool hasUpdatedGrid;
    private int waypointIndex;

    private void Awake()
    {
        CreateGrid();
    }

    public void SubscribeToGrid(Waypoint waypoint)
    {
        if (waypoints.Contains(waypoint)) return;
        waypoints.Add(waypoint);
    }

    public void UnsubscribeFromGrid(Waypoint waypoint)
    {
        if (!waypoints.Contains(waypoint)) return;
        waypoints.Remove(waypoint);
    }

    public Waypoint GetRandomWaypoint()
    {
        var randomIndex = Random.Range(0, waypoints.Count);
        return waypoints[randomIndex];
    }

    private void CalculateWaypoints()
    {
        waypointAmount.x = (int)Mathf.Abs(GridSize.x);
        waypointAmount.y = (int)Mathf.Abs(GridSize.y);
    }

    private void InitPosition()
    {
        ResetXPosition();
        InitYPosition();
    }

    private void ResetXPosition()
    {
        initPosition.x = -(GridSize.x - 1) / 2;
        safeZone.x = -initPosition.x - initPosition.x + 1;
    }

    private void InitYPosition()
    {
        initPosition.y = (GridSize.y - 1) / 2;
        safeZone.y = initPosition.y - -initPosition.y + 1;
    }

    private void DeleteGrid()
    {
        waypoints.Clear();

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void CreateLine()
    {
        InitYPosition();
        for (int i = 0; i < waypointAmount.y; i++)
        {
            var newWaypoint = Instantiate(waypointPrefab, initPosition, transform.rotation);
            newWaypoint.transform.parent = transform;
            newWaypoint.SetGrid(this);
            newWaypoint.name += $"{++waypointIndex}";
            initPosition.y -= safeZone.y / waypointAmount.y;
        }
    }

    private void CreateGrid()
    {
        waypointIndex = 0;
        CalculateWaypoints();
        DeleteGrid();
        InitPosition();
        
        for (int i = 0; i < waypointAmount.x; i++)
        {
            CreateLine();
            initPosition.x += safeZone.x / waypointAmount.x;
        }
    }

    private void OnDrawGizmos()
    {
        CalculateWaypoints();
        
        if (updateGrid)
        {
            CreateGrid();
            updateGrid = false;
        }

        Gizmos.DrawWireCube(transform.position, GridSize);
    }
}
