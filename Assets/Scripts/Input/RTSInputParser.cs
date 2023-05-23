using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSInputParser : MonoBehaviour
{
    [SerializeField] private ShipMovement shipMovement;
    [SerializeField] private PlayerMovement playerMovement;

    private Vector3 mousePosition;
    private Vector3 inputMovement;
    private Plane plane = new Plane(Vector3.up,0);

    protected void Start()
    {
        if (shipMovement != null)
        {
            //ControlsActions["SetShipDestination"].performed += SetTargetDestination;
        }
        
        /*if (playerMovement != null)
        {
            _rtsControlsActions["InputMovement"].performed += MovePlayer;
        }*/

        //EnableInput();
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
        /*mousePosition = ControlsActions["MousePosition"].ReadValue<Vector2>();

        if (playerMovement != null)
        {
            inputMovement = ControlsActions["CameraMovement"].ReadValue<Vector2>();
            var input3D = new Vector3(inputMovement.x, 0, inputMovement.y);
            
            playerMovement.MovePlayer(input3D);
        }*/
    }

    private void OnDestroy()
    {
        //ControlsActions["SetShipDestination"].performed -= SetTargetDestination;
    }
}
