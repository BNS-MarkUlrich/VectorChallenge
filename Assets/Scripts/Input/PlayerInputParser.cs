using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputParser : InputParser
{
    private Interactor interactor;
    
    [Header("Player")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("MoveInput")]
    private Vector3 _inputMovement;

    private void Awake()
    {
        interactor = GetComponent<Interactor>();
    }

    private void FixedUpdate()
    {
        switch (CurrentActionMap.name)
        {
            // Player
            case "Player":
                MovePlayer(ReadMoveInput());
                RotateCamera(GetMouseDelta());
                break;
        }
    }

    protected override void AddListeners()
    {
        ControlsActions["Interact"].performed += Interact;
    }

    protected override void RemoveListeners()
    {
        ControlsActions["Interact"].performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        interactor.Interact();
    }

    // Player
    private void MovePlayer(Vector3 moveInput)
    {
        playerMovement.MovePlayer(moveInput);
    }

    // MoveInput
    private Vector3 ReadMoveInput()
    {
        var input3D = ControlsActions["Movement"].ReadValue<Vector2>();
        _inputMovement.Set(input3D.x, 0, input3D.y);

        return _inputMovement;
    }
    
    private Vector2 GetMouseDelta()
    {
        return ControlsActions["MouseDelta"].ReadValue<Vector2>();
    }
    
    private void RotateCamera(Vector2 rotationDelta)
    {
        playerMovement.RotatePlayer(rotationDelta);
    }
}
