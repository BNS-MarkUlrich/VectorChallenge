using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CommandTerminal : Interactable
{
    [Header("Applied Scripts")]
    [SerializeField] private InputParser targetInputObject;
    private InputParser userInputParser;

    /*[Header("Display")]
    [TextArea(4,0)]
    [SerializeField] private string inputField;
    [SerializeField] private Text displayText;*/
    
    [Header("Unity Events")]
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
