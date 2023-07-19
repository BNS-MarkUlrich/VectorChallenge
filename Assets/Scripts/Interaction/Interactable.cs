using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Interactor OriginInteractor;

    public void SetInteractor(Interactor newInteractor)
    {
        OriginInteractor = newInteractor;
    }

    public abstract void Interact();
}
