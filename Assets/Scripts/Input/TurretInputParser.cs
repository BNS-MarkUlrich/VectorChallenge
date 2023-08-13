using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class TurretInputParser : InputParser
{
    [Header("Applied Scripts")]
    [SerializeField] private Turret _turret;
    [SerializeField] private ManualFlightMovement manualFlightMovement;
    [SerializeField] private FPCameraController _fpCameraController;
    [SerializeField] private CommandTerminal commandTerminal;

    [Header("Input Variables")]
    [SerializeField] private float cameraBoundsRadius = 15;
    [SerializeField] private float cameraSnapRadius = 1;
    [SerializeField] private bool ignorePitch =  true;

    protected override void InitInput()
    {
        base.InitInput();
        _turret.IsAutomaticTurret = false;
    }

    protected override void AddListeners(out bool hasListeners)
    {
        ControlsActions["Disconnect"].performed += Disconnect;
        ControlsActions["Shoot"].performed += Shoot;
        hasListeners = true;
    }

    protected override void RemoveListeners()
    {
        if (!HasListeners) return;

        ControlsActions["Disconnect"].performed -= Disconnect;
        ControlsActions["Shoot"].performed -= Shoot;
    }

    private void Awake()
    {
        if (commandTerminal == null)
        {
            commandTerminal = GetComponentInChildren<CommandTerminal>();
        }
    }
    
    private void FixedUpdate()
    {
        RotateTurret(GetMouseDelta());
    }

    private void RotateTurret(Vector2 rotationDelta)
    {
        _fpCameraController.LookRotationClamped(rotationDelta, cameraBoundsRadius, cameraSnapRadius);
        manualFlightMovement.ApplyTurningThrust(_fpCameraController.NormalizedVelocity, ignorePitch);
    }
    
    private void Disconnect(InputAction.CallbackContext context)
    {
        commandTerminal.Disconnect();
    }
    
    private void Shoot(InputAction.CallbackContext context)
    {
        _turret.Fire();
    }
    
    // Todo: Add Zoom Function (which also increases/specifies aim assist?
    // Todo#2: When in manual mode, aim assist is limited to camera frustum 
}
