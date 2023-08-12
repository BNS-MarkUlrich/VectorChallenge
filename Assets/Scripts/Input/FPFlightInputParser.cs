using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPFlightInputParser : InputParser
{
    [Header("Applied Scripts")]
    [SerializeField] protected ManualFlightMovement manualFlightMovement;
    [SerializeField] protected FPCameraController fpCameraController;
    [SerializeField] private CommandTerminal commandTerminal;

    [Header("Input Variables")]
    [SerializeField] private float cameraClampRadius;
    private Vector2 inputMovement;
    private Vector2 mouseDelta;

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
        ApplyTurningThrust(GetMouseDelta(), cameraClampRadius);
    }

    private void ApplyForwardThrust(float thrust)
    {
        manualFlightMovement.ApplyForwardThrust(thrust);
    }
    
    private void ApplyTurningThrust(Vector2 thrust, float radius)
    {
        fpCameraController.LookRotationClamped(thrust, radius);
        manualFlightMovement.ApplyTurningThrust(fpCameraController.CameraVelocity);
    }
    
    private void Disconnect(InputAction.CallbackContext context)
    {
        commandTerminal.Disconnect();
    }
}
