using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _interactorRange;
    [SerializeField] private LayerMask _layerMask;
    private CharacterController characterController;
    private Ray ray;
    private RaycastHit hit;
    private bool hasHit;

    private void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        var isHitting = Physics.Raycast(ray, out hit, _interactorRange, _layerMask);

        if (isHitting)
        {
            if (!hasHit)
            {
                print(hit.transform.name);
            }
            
            hasHit = true;
        }
        else
        {
            hasHit = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * _interactorRange);
    }
}
