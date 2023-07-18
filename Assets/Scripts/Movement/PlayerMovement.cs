using System;
using UnityEngine;

public class PlayerMovement : Movement
{
    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private Camera _myCamera;
    private bool groundedPlayer;

    private CharacterController characterController;
    private Vector3 playerVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += _gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void MovePlayer(Vector3 input)
    {
        var velocity = input * (Time.deltaTime * maxSpeed);
        velocity.y = MyRigidBody.velocity.y;

        if (input != Vector3.zero)
        {
            //MyRigidBody.velocity = velocity;
            characterController.Move(transform.TransformVector(velocity));
        }
    }

    public void RotatePlayer(Vector2 input)
    {
        Transform myTransform;
        (myTransform = transform).Rotate(0, input.x, 0);
        var eulerAngles = myTransform.eulerAngles;
        var z = eulerAngles.z;
        var x = eulerAngles.x;
        transform.Rotate(-x, 0, -z);
        
        _myCamera.transform.Rotate(-input.y,0,0);
    }
}
