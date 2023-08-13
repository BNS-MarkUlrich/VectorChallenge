using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FPCameraController : MonoBehaviour
{
    [SerializeField] private Camera _myCamera;
    
    private Vector2 cameraRotation;
    private Vector2 clampedCameraRotation;
    private float clampDistance;
    private Vector2 normalizedVelocity;

    public Vector2 NormalizedVelocity => normalizedVelocity;

    public void RotateHorizontally(Vector2 input)
    {
        cameraRotation.x += -input.y;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -60f, 60f);

        transform.Rotate(Vector3.up * input.x);
        
        _myCamera.transform.localRotation = Quaternion.Euler(cameraRotation.x, 0, 0);
    }
    
    public void LookRotation(Vector2 input)
    {
        cameraRotation += input;
        
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -60f, 60f);

        transform.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);
    }
    
    public void LookRotationClamped(Vector2 input, float radius, float snapRadius)
    {
        cameraRotation += input;

        /*if (cameraRotation.magnitude <= 0.1f)
        {
            cameraRotation = Vector2.zero;
        }*/

        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -radius, radius);
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -radius, radius);

        clampedCameraRotation = cameraRotation;

        clampDistance = cameraRotation.magnitude;

        if (clampDistance < snapRadius)
        {
            clampedCameraRotation = Vector2.zero;
        }

        normalizedVelocity = GetNormalizedVelocity(cameraRotation, radius);
        
        transform.localRotation = Quaternion.Euler(-clampedCameraRotation.y, clampedCameraRotation.x, 0);
    }

    private static Vector2 GetNormalizedVelocity(Vector2 velocity, float maxDelta)
    {
        return velocity / maxDelta;
    }
}
