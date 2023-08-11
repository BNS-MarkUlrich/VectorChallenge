using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualFlightInputParser : InputParser
{
    [Header("Applied Scripts")]
    [SerializeField] protected ManualFlightMovement manualFlightMovement;

    [Header("Input Variables")]
    [SerializeField] private Vector2 inputMovement;
    
    protected override void AddListeners(out bool hasListeners)
    {
        
        hasListeners = false;
    }

    protected override void RemoveListeners()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyThrust(ReadMoveInput().y);
    }

    private Vector3 ReadMoveInput()
    {
        inputMovement = ControlsActions["Movement"].ReadValue<Vector2>();
        return inputMovement;
    }

    private void ApplyThrust(float thrust)
    {
        manualFlightMovement.ApplyThrust(thrust);
    }
}
