using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();

    [Header("Grid Creation")]
    [SerializeField] private Waypoint waypointPrefab;
    [SerializeField] private Vector2 GridSize;
    [SerializeField] private bool updateGrid;
    [SerializeField] private bool clearGrid;

    private GameObject waypointsParent;
    private Vector2 initPosition;
    private Vector2 safeZone;
    private Vector2 waypointAmount;
    private bool hasUpdatedGrid;
    private int waypointIndex;

    private void Awake()
    {
        updateGrid = false;

        if (waypoints.Count <= 0)
        {
            CreateGrid();
        }
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
        ResetYPosition();
    }

    private void ResetXPosition()
    {
        initPosition.x = -(GridSize.x - 1) / 2;
        safeZone.x = -initPosition.x - initPosition.x + 1;
    }

    private void ResetYPosition()
    {
        initPosition.y = (GridSize.y - 1) / 2;
        safeZone.y = initPosition.y - -initPosition.y + 1;
    }

    private void DeleteGrid()
    {
        waypoints.Clear();

        for (int i = waypointsParent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(waypointsParent.transform.GetChild(i).gameObject);
        }
    }

    private void CreateLine()
    {
        ResetYPosition();
        for (int i = 0; i < waypointAmount.y; i++)
        {
            var newWaypoint = Instantiate(waypointPrefab, transform.position, transform.rotation);
            newWaypoint.transform.parent = waypointsParent.transform;
            newWaypoint.transform.localPosition = initPosition;
            newWaypoint.SetGrid(this);
            newWaypoint.name += $"{++waypointIndex}";
            initPosition.y -= safeZone.y / waypointAmount.y;
        }
    }

    private void CreateWaypointsParent()
    {
        if (waypointsParent != null) return;

        waypointsParent = new GameObject("WaypointParent");
        waypointsParent.transform.parent = transform;
        waypointsParent.transform.localPosition = Vector3.zero;
    }

    private void CreateGrid()
    {
        waypointIndex = 0;
        CreateWaypointsParent();
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
            updateGrid = false;
            CreateGrid();
        }

        if (clearGrid)
        {
            clearGrid = false;
            DeleteGrid();
        }

        Gizmos.DrawWireCube(transform.position, GridSize);
    }
}
