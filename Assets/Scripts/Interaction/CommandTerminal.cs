using System;
using UnityEngine;

public class CommandTerminal : Interactable
{
    private InputParser targetInputObject;
    private InputParser originInputParser;
    private bool isInUse;

    private void Start()
    {
        targetInputObject = GetComponentInParent<InputParser>();
    }

    public override void Interact()
    {
        if (isInUse) return;

        if (!OriginInteractor.TryGetComponent(out originInputParser)) return;

        originInputParser.SwitchInput(targetInputObject);
        isInUse = true;
    }

    public override void Disconnect()
    {
        targetInputObject.SwitchInput(originInputParser);
        isInUse = false;
    }
}
