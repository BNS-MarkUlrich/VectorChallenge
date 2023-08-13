using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualFlightInputParser : InputParser
{
    [Header("Applied Scripts")]
    [SerializeField] protected ManualFlightMovement manualFlightMovement;
    [SerializeField] protected RTSCameraMovement rtsCameraMovement;
    [SerializeField] private CommandTerminal commandTerminal;

    [Header("Input Variables")]
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private bool ignorePitch = true;
    private Vector2 mouseDelta;

    protected override void OnEnable()
    {
        base.OnEnable();
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
        ApplyLateralThrust(ReadMoveInput().x);
        //ApplyTurningThrust(ReadMoveInput()); Mark: Testing some stuff
        RotateCamera(GetMouseDelta());
        ZoomCamera(GetScrollDelta());
        rtsCameraMovement.LockToTarget();
    }

    private Vector2 ReadMoveInput()
    {
        inputMovement = ControlsActions["Movement"].ReadValue<Vector2>();
        return inputMovement;
    }
    
    private Vector2 GetScrollDelta()
    {
        return ControlsActions["ScrollZoom"].ReadValue<Vector2>();
    }

    private void ApplyForwardThrust(float thrust)
    {
        manualFlightMovement.ApplyForwardThrust(thrust);
    }
    
    private void ApplyLateralThrust(float thrust)
    {
        manualFlightMovement.ApplyLateralThrust(thrust);
    }
    
    private void ApplyTurningThrust(Vector2 thrust)
    {
        manualFlightMovement.ApplyTurningThrust(thrust, ignorePitch);
    }

    private void RotateCamera(Vector2 rotationDelta)
    {
        rtsCameraMovement.RotateRTSCamera(rotationDelta);
    }
    
    private void ZoomCamera(Vector2 zoomDelta)
    {
        rtsCameraMovement.ZoomRTSCamera(zoomDelta);
    }
    
    private void Disconnect(InputAction.CallbackContext context)
    {
        commandTerminal.Disconnect();
    }
}
