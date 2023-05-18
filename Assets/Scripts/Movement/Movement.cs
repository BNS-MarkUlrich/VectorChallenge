using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float speed = 2f;
    
    protected Rigidbody MyRigidBody;
    protected Vector3 Velocity => MyRigidBody.velocity;
    protected float Mass => MyRigidBody.mass;
    
    private void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody>();
    }
}
