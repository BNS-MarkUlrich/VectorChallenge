using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputParser : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputActionAsset _rtsControlsActions;

    [SerializeField] private ShipMovement shipMovement;

    private Vector3 mousePosition;
    private Plane plane = new Plane(Vector3.up,0);

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rtsControlsActions = _playerInput.actions;

        shipMovement = GetComponent<ShipMovement>();
        
        _rtsControlsActions["SetShipDestination"].performed += SetTargetDestination;
        
        _rtsControlsActions.Enable();
    }

    private void SetTargetDestination(InputAction.CallbackContext context)
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mousePosition = ray.GetPoint(distance);
        }
        
        shipMovement.SetTargetDestination(mousePosition);
    }

    private void FixedUpdate()
    {
        mousePosition = _rtsControlsActions["MousePosition"].ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        _rtsControlsActions["SetShipDestination"].performed -= SetTargetDestination;
    }
}
