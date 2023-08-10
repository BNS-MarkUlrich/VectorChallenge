using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected bool _lockInPlace = true;

    private Vector3 originalPosition; // Temp
    protected Interactor UserInteractor;
    
    protected bool IsInUse;

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
        if (IsInUse) return;
        
        UserInteractor = newInteractor;
        
        IsInUse = true;
    }

    public abstract void Interact();

    public abstract void Disconnect();

    private void LockInPlace()
    {
        transform.position = originalPosition;
    }
}
