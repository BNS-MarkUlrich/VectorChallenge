using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected Vector3 velocity;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float turnTimer = 5f;
    
    protected Rigidbody Rigidbody;
}
