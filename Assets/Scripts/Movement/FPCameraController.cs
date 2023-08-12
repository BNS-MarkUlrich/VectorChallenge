using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FPCameraController : MonoBehaviour
{
    [SerializeField] private Camera _myCamera;
    [SerializeField] private Vector2 cameraRotation;
    [SerializeField] private Vector2 clampedCameraRotation;
    [SerializeField] private float clampDistance;
    [SerializeField] private Vector2 normalizedVector;

    public Vector2 CameraVelocity => normalizedVector;

    public void RotateHorizontally(Vector2 input)
    {
        cameraRotation.x += -input.y;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -60f, 60f);

        transform.Rotate(Vector3.up * input.x);
        
        _myCamera.transform.localRotation = Quaternion.Euler(cameraRotation.x, 0, 0);
    }
    
    public void LookRotation(Vector2 input)
    {
        cameraRotation.x += -input.y;
        cameraRotation.y += input.x;
        
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -60f, 60f);

        transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);
    }
    
    public void LookRotationClamped(Vector2 input, float radius)
    {
        cameraRotation += input;

        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -radius, radius);
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -radius, radius);

        clampedCameraRotation = cameraRotation;

        clampDistance = cameraRotation.magnitude;

        if (clampDistance < radius / radius)
        {
            clampedCameraRotation = Vector2.zero;
        }
        
        normalizedVector = clampedCameraRotation / radius;
        
        transform.localRotation = Quaternion.Euler(-clampedCameraRotation.y, clampedCameraRotation.x, 0);
    }
}
