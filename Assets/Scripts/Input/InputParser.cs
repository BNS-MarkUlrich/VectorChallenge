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
    private bool isRefocusingTarget;

    [Header("MoveInput")]
    private Vector3 _inputMovement;

    public bool IsRefocusingTarget
    {
        get => isRefocusingTarget;
        set => isRefocusingTarget = value;
    }

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
                ZoomRTSCamera(GetScrollDelta());
                if (_controlsActions["ActivateRotation"].inProgress && activateCameraRotation)
                {
                    RotateRTSCamera(GetMouseDelta());
                    return;
                }

                if (_controlsActions["FocusOnTarget"].inProgress || isRefocusingTarget)
                {
                    FocusOnTarget();
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
    
    private Vector2 GetScrollDelta()
    {
        return _controlsActions["ScrollZoom"].ReadValue<Vector2>();
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

    private Vector3 CameraCenterToWorldPos()
    {
        float distance;
        var worldPos = Vector3.zero;
        var ray1 = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        var plane = new Plane(Vector3.up,0);
        if (plane.Raycast(ray1, out distance))
        {
            worldPos = ray1.GetPoint(distance);
        }

        return worldPos;
    }
    
    private void SetTargetDestination(InputAction.CallbackContext context)
    {
        if (activateCameraRotation) return; // Remove later
        shipMovement.SetTargetDestination(CalculateMouseWorldPosition());
    }
    
    private void SetRotationTarget(InputAction.CallbackContext context)
    {
        activateCameraRotation = true;
        _mousePosition = CameraCenterToWorldPos();
    }
    
    private void FocusOnTarget()
    {
        isRefocusingTarget = true;
        _rtsCameraMovement.FocusOnTarget();
    }

    private void MoveRTSCamera(Vector3 moveInput)
    {
        _rtsCameraMovement.MoveRTSCamera(moveInput);
    }
    
    private void RotateRTSCamera(Vector2 rotationDelta) 
    {
        _rtsCameraMovement.RotateRTSCamera(rotationDelta);
    }
    
    private void ZoomRTSCamera(Vector2 zoomDelta)
    {
        _rtsCameraMovement.ZoomRTSCamera(zoomDelta);
    }
    
    private void RemoveRTSListeners()
    {
        _controlsActions["SetShipDestination"].performed -= SetTargetDestination;
    }

    // MoveInput
    private Vector3 ReadMoveInput()
    {
        var input3D = _controlsActions["Movement"].ReadValue<Vector2>();
        _inputMovement.Set(input3D.x, input3D.y/2, input3D.y);

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
