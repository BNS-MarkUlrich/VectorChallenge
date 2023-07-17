using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwitcher : InputParser
{
    public void SetInputActionMap(string inputType)
    {
        PlayerInput.currentActionMap = ControlsActions.FindActionMap(inputType);
    }

    protected override void AddListeners()
    {
        
    }

    protected override void RemoveListeners()
    {
        
    }
}
