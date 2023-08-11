using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CommandTerminal : Interactable
{
    [SerializeField] private InputParser targetInputObject;
    private InputParser userInputParser;

    [SerializeField] private UnityEvent onConnect;
    [SerializeField] private UnityEvent onDisconnect;

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
        
        onConnect?.Invoke();
    }

    public override void Disconnect()
    {
        targetInputObject.SwitchInput(userInputParser);
        userInputParser = null;
        IsInUse = false;
        
        onDisconnect?.Invoke();
    }
}
