using System;
using UnityEngine;

public class CommandTerminal : Interactable
{
    private InputParser targetInputObject;
    private InputParser originInputParser;

    private void Start()
    {
        targetInputObject = GetComponentInParent<InputParser>();
    }

    public override void Interact()
    {
        if (!OriginInteractor.TryGetComponent(out originInputParser)) return;

        originInputParser.SwitchInput(targetInputObject);
    }

    public override void Disconnect()
    {
        targetInputObject.SwitchInput(originInputParser);
        IsInUse = false;
    }
}
