using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputParser : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputActionAsset _controlsActions;
    private InputActionMap CurrentActionMap => _playerInput.currentActionMap;

    [Header("Player")]
    [SerializeField] private PlayerMovement playerMovement;
    
    [Header("RTS")]
    [SerializeField] private RTSCameraMovement _rtsCameraMovement;
    [SerializeField] private ShipMovement shipMovement;
    private Vector3 _mousePosition;
    private bool activateCameraRotation;
    
    [Header("MoveInput")]
    private Vector3 _inputMovement;

    private void Start()
    {
        InitInput();
        
        AddRTSListeners();
        
        SetInputActionMap("RTS");
    }
    
    private void FixedUpdate()
    {
        switch (CurrentActionMap.name)
        {
            // Player
            case "Player":
                MovePlayer(ReadMoveInput());
                break;
            // RTS
            case "RTS":
                MoveRTSCamera(ReadMoveInput());
                if (_controlsActions["ActivateRotation"].inProgress && activateCameraRotation)
                {
                    RotateRTSCamera(_mousePosition, GetMouseDelta());
                    return;
                }

                activateCameraRotation = false;
                FollowMousePosition();
                break;
        }
    }
    
    public void SetInputActionMap(string inputType)
    {
        _playerInput.currentActionMap = _controlsActions.FindActionMap(inputType);
    }

    private void InitInput()
    {
        _playerInput = GetComponent<PlayerInput>();
        _controlsActions = _playerInput.actions;
        
        _controlsActions.Enable();
    }
    
    // Player
    private void MovePlayer(Vector3 moveInput)
    {
        playerMovement.MovePlayer(moveInput);
    }

    // RTS
    private void AddRTSListeners()
    {
        _controlsActions["SetShipDestination"].performed += SetTargetDestination;
        _controlsActions["ActivateRotation"].performed += SetRotationTarget;
    }
    
    private void FollowMousePosition()
    {
        _mousePosition = _controlsActions["MousePosition"].ReadValue<Vector2>();
    }

    private Vector2 GetMouseDelta()
    {
        return _controlsActions["MouseDelta"].ReadValue<Vector2>();
    }

    private Vector3 CalculateMouseWorldPosition()
    {
        float distance;
        var ray = Camera.main!.ScreenPointToRay(_mousePosition);
        var plane = new Plane(Vector3.up,0);
        if (plane.Raycast(ray, out distance))
        {
            _mousePosition = ray.GetPoint(distance);
        }

        return _mousePosition;
    }
    
    private void SetTargetDestination(InputAction.CallbackContext context)
    {
        if (activateCameraRotation) return; // Remove later
        shipMovement.SetTargetDestination(CalculateMouseWorldPosition());
    }
    
    private void SetRotationTarget(InputAction.CallbackContext context)
    {
        activateCameraRotation = true;
        CalculateMouseWorldPosition();
    }
    
    private void MoveRTSCamera(Vector3 moveInput)
    {
        _rtsCameraMovement.MoveRTSCamera(moveInput);
    }
    
    private void RotateRTSCamera(Vector3 rotatePoint, Vector2 rotationDelta) 
    {
        _rtsCameraMovement.RotateRTSCamera(rotatePoint, rotationDelta);
    }
    
    private void RemoveRTSListeners()
    {
        _controlsActions["SetShipDestination"].performed -= SetTargetDestination;
    }

    // MoveInput
    private Vector3 ReadMoveInput()
    {
        var input3D = _controlsActions["Movement"].ReadValue<Vector2>();
        _inputMovement.Set(input3D.x, 0, input3D.y);

        return _inputMovement;
    }

    private void RemoveAllListeners()
    {
        RemoveRTSListeners();
    }

    private void OnDestroy()
    {
        RemoveAllListeners();
    }
}
