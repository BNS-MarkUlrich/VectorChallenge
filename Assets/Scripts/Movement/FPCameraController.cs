using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FPCameraController : MonoBehaviour
{
    [SerializeField] private Camera _myCamera;

    private float xRotation;
    private float yRotation;

    public void RotateHorizontally(Vector2 input)
    {
        xRotation += -input.y;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.Rotate(Vector3.up * input.x);
        
        _myCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
    
    public void LookAtRotation(Vector2 input)
    {
        xRotation += -input.y;
        yRotation += input.x;
        
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    
    public Quaternion LookAtRotationClamped(Vector2 input, float radius)
    {
        xRotation += -input.y;
        yRotation += input.x;
        
        xRotation = Mathf.Clamp(xRotation, -radius, radius);
        yRotation = Mathf.Clamp(yRotation, -radius, radius);

        return transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
