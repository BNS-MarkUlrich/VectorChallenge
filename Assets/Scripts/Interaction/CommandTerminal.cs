using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CommandTerminal : Interactable
{
    [SerializeField] protected InputParser _targetInputObject;
    private bool isInUse;
    
    public override void Interact()
    {
        if (isInUse) return;

        if (!OriginInteractor.TryGetComponent(out InputParser inputParser)) return;
        
        inputParser.SwitchInput(_targetInputObject);
    }
}
