using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class FPFlightInputParser : InputParser
{
    [Header("Applied Scripts")]
    [SerializeField] protected ManualFlightMovement manualFlightMovement;
    [SerializeField] protected FPCameraController fpCameraController;
    [SerializeField] private CommandTerminal commandTerminal;
    
    [Header("Input Variables")]
    [SerializeField] private float cameraBoundsRadius = 15;
    [SerializeField] private float cameraSnapRadius = 1;
    [SerializeField] private bool ignorePitch;
    private Vector2 inputMovement;
    private Vector2 mouseDelta;

    private bool isMovingMouse;
    
    private void Awake()
    {
        if (commandTerminal == null)
        {
            commandTerminal = GetComponentInChildren<CommandTerminal>();
        }
    }

    protected override void AddListeners(out bool hasListeners)
    {
        ControlsActions["Disconnect"].performed += Disconnect;
        hasListeners = true;
    }

    protected override void RemoveListeners()
    {
        if (!HasListeners) return;

        ControlsActions["Disconnect"].performed -= Disconnect;
    }

    private void FixedUpdate()
    {
        ApplyForwardThrust(ReadMoveInput().y);
        ApplyPitchThrust(ReadMoveInput().x);
        ApplyTurningThrust(GetMouseDelta());
    }
    
    private Vector2 ReadMoveInput()
    {
        inputMovement = ControlsActions["Movement"].ReadValue<Vector2>();
        return inputMovement;
    }

    private void ApplyForwardThrust(float thrust)
    {
        manualFlightMovement.ApplyForwardThrust(thrust);
    }
    
    private void ApplyPitchThrust(float thrust)
    {
        manualFlightMovement.ApplyPitchThrust(thrust);
    }
    
    private void ApplyTurningThrust(Vector2 thrust)
    {
        fpCameraController.LookRotationClamped(thrust, cameraBoundsRadius, cameraSnapRadius);
        manualFlightMovement.ApplyTurningThrust(fpCameraController.NormalizedVelocity, ignorePitch);
    }
    
    private void Disconnect(InputAction.CallbackContext context)
    {
        commandTerminal.Disconnect();
    }
}
