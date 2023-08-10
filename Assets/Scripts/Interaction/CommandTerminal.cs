using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CommandTerminal : Interactable
{
    [SerializeField] private InputParser targetInputObject;
    private InputParser userInputParser;

    private void Start()
    {
        if (targetInputObject == null)
        {
            targetInputObject = GetComponentInParent<InputParser>();
        }
    }

    public override void Interact()
    {
        if (!UserInteractor.TryGetComponent(out userInputParser)) return;
        
        userInputParser.SwitchInput(targetInputObject);
    }

    public override void Disconnect()
    {
        targetInputObject.SwitchInput(userInputParser);
        userInputParser = null;
        IsInUse = false;
    }
}
