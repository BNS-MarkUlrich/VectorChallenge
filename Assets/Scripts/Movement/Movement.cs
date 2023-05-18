using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected Vector3 velocity;
    [SerializeField] protected float speed = 2f;

    protected Rigidbody Rigidbody;
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
}
