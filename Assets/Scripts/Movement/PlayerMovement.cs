using UnityEngine;

public class PlayerMovement : Movement
{
    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private Camera _myCamera;
    private bool groundedPlayer;

    public void MovePlayer(Vector3 input)
    {
        var velocity = input * maxSpeed;

        if (input != Vector3.zero)
        {
            MyRigidBody.velocity = velocity;
        }
    }

    public void RotatePlayer(Vector2 input)
    {
        transform.Rotate(0, input.x, 0);
        _myCamera.transform.Rotate(-input.y, input.x, 0);
        var z = _myCamera.transform.eulerAngles.z;
        _myCamera.transform.Rotate(0, 0, -z);
    }
}
