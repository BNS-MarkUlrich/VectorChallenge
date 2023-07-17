using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputParser : InputParser
{
    [Header("Player")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("MoveInput")]
    private Vector3 _inputMovement;

    private void FixedUpdate()
    {
        switch (CurrentActionMap.name)
        {
            // Player
            case "Player":
                MovePlayer(ReadMoveInput());
                break;
        }
    }

    protected override void AddListeners()
    {
        
    }

    protected override void RemoveListeners()
    {
        
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
        _inputMovement.Set(input3D.x, input3D.y / 2, input3D.y);

        return _inputMovement;
    }
}
