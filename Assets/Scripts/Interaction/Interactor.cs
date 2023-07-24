using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _interactorRange;
    [SerializeField] private LayerMask _layerMask;

    private bool canInteract;

    private CharacterController characterController;
    private Interactable currentInteractable;

    private RaycastHit hit;
    private Ray ray;

    private void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        canInteract = Physics.Raycast(ray, out hit, _interactorRange, _layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * _interactorRange);
    }

    public void Interact()
    {
        if (!canInteract) return;

        hit.collider.transform.TryGetComponent(out currentInteractable);

        currentInteractable.SetInteractor(this);
        currentInteractable.Interact();
    }
}
