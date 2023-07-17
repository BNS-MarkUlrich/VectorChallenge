using UnityEngine;

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
                RotateCamera(GetMouseDelta());
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
