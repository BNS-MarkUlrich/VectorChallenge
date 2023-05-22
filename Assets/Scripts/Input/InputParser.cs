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
    [SerializeField] private PlayerMovement playerMovement;

    private Vector3 mousePosition;
    private Vector3 inputMovement;
    private Plane plane = new Plane(Vector3.up,0);

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rtsControlsActions = _playerInput.actions;

        //shipMovement = GetComponent<ShipMovement>();
        if (shipMovement != null)
        {
            _rtsControlsActions["SetShipDestination"].performed += SetTargetDestination;
        }
        
        /*if (playerMovement != null)
        {
            _rtsControlsActions["InputMovement"].performed += MovePlayer;
        }*/
        
        
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

    /*private void MovePlayer(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
        var input3D = new Vector3(inputMovement.x, 0, inputMovement.y);
        
        playerMovement.MovePlayer(input3D);
    }*/

    private void FixedUpdate()
    {
        mousePosition = _rtsControlsActions["MousePosition"].ReadValue<Vector2>();

        if (playerMovement != null)
        {
            inputMovement = _rtsControlsActions["InputMovement"].ReadValue<Vector2>();
            var input3D = new Vector3(inputMovement.x, 0, inputMovement.y);
        
            playerMovement.MovePlayer(input3D);
        }
    }

    private void OnDestroy()
    {
        _rtsControlsActions["SetShipDestination"].performed -= SetTargetDestination;
    }
}
