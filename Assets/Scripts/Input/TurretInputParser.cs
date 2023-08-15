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
    [SerializeField] private FPCameraController fpCameraController;
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

    protected override void OnEnable()
    {
        base.OnEnable();
        MakeOwnParent(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MakeOwnParent(false);
    }

    protected void MakeOwnParent(bool makeOwnParent)
    {
        if (makeOwnParent)
        {
            transform.SetParent(transform.root.parent);
            return;
        }
        
        Transform myTransform;
        (myTransform = transform).SetParent(_turret.transform);
        myTransform.localPosition = Vector3.zero;
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
            commandTerminal = _turret.GetComponentInChildren<CommandTerminal>();
        }
    }
    
    private void FixedUpdate()
    {
        RotateTurret(GetMouseDelta());

        FollowTurretPosition();
    }

    private void FollowTurretPosition()
    {
        transform.position = _turret.transform.position;
    }

    private void RotateTurret(Vector2 rotationDelta)
    {
        fpCameraController.LookRotationClamped(rotationDelta, cameraBoundsRadius, cameraSnapRadius, _turret.AimAssist);
        manualFlightMovement.RotateTowards(fpCameraController.transform.rotation);
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
    // Todo#3: Add (better) Camera stabiliser
}
