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
    private bool canInteract;

    private InputParser myInputParser;
    private Interactable currentInteractable;

    private void Awake()
    {
        myInputParser = GetComponent<InputParser>();
    }

    private void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        canInteract = Physics.Raycast(ray, out hit, _interactorRange, _layerMask);
    }

    public void Interact()
    {
        if (!canInteract) return;

        hit.transform.TryGetComponent(out currentInteractable);

        currentInteractable.SetInteractor(this);
        currentInteractable.Interact();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * _interactorRange);
    }
}
