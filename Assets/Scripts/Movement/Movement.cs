using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float mass = 1f;
    [SerializeField] protected float speed = 2f;
    
    protected Rigidbody MyRigidBody;
    public Vector3 Velocity => MyRigidBody.velocity;
    
    private void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody>();
    }
}
