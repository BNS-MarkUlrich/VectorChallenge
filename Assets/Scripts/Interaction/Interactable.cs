using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected bool _lockInPlace = true;

    private Vector3 originalPosition; // Temp
    protected Interactor OriginInteractor;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (!_lockInPlace) return;

        LockInPlace();
    }

    public void SetInteractor(Interactor newInteractor)
    {
        OriginInteractor = newInteractor;
    }

    public abstract void Interact();

    public abstract void Disconnect();

    private void LockInPlace()
    {
        transform.position = originalPosition;
    }
}
