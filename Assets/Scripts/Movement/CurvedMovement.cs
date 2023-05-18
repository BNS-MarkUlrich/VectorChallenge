using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedMovement : Movement
{
    [SerializeField] private Transform target;
    private Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Velocity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t =  (Vector3.Normalize(target.position - rigidbody.position) + Vector3.Normalize(rigidbody.velocity)) * speed;
        
        rigidbody.position += rigidbody.velocity.magnitude * t * Time.deltaTime;
    }
}
