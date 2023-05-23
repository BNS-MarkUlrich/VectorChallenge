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
    [SerializeField] private ShipMovement shipMovement;
    private Vector3 _mousePosition;
    
    [Header("MoveInput")]
    private Vector3 _inputMovement;

    private void Start()
    {
        InitInput();
        
        AddRTSListeners();
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
                FollowMousePosition();
                break;
        }
    }
    
    public void SetInputActionMap(InputActionMap inputType)
    {
        _playerInput.currentActionMap = inputType;
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
    }
    
    private void FollowMousePosition()
    {
        _mousePosition = _controlsActions["MousePosition"].ReadValue<Vector2>();
    }
    
    private void SetTargetDestination(InputAction.CallbackContext context)
    {
        float distance;
        var ray = Camera.main!.ScreenPointToRay(_mousePosition);
        var plane = new Plane(Vector3.up,0);
        if (plane.Raycast(ray, out distance))
        {
            _mousePosition = ray.GetPoint(distance);
        }
        
        shipMovement.SetTargetDestination(_mousePosition);
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
