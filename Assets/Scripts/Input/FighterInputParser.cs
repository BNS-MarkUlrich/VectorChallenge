using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class FighterInputParser : FPFlightInputParser
{
    [SerializeField] private List<Turret> turrets;

    protected override void AddListeners(out bool hasListeners)
    {
        ControlsActions["Shoot"].performed += Shoot;
        base.AddListeners(out hasListeners);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ControlsActions["Shoot"].performed -= Shoot;
    }
    
    private void Shoot(InputAction.CallbackContext context)
    {
        foreach (var turret in turrets)
        {
            turret.Fire();
        }
    }
}
