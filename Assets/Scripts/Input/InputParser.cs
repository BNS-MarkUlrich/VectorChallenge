using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public abstract class InputParser : MonoBehaviour
{
    protected InputActionAsset ControlsActions;
    protected PlayerInput PlayerInput;
    protected InputActionMap CurrentActionMap => PlayerInput.currentActionMap;
    
    [SerializeField] protected InputParser _switchInputObject;
    [SerializeField] protected InputTypes _inputActionMap;
    
    private void OnEnable()
    {
        InitInput();
        
        AddListeners();
        ControlsActions["SwitchInput"].performed += SwitchInputContext;
    }
    
    private void InitInput()
    {
        PlayerInput = GetComponentInChildren<PlayerInput>();
        ControlsActions = PlayerInput.actions;

        ControlsActions.Enable();
    }
    
    public void SetInputActionMap(string inputType)
    {
        PlayerInput.currentActionMap = ControlsActions.FindActionMap(inputType);
    }

    public void SwitchInput(InputParser target = null)
    {
        if (target == null)
        {
            target = _switchInputObject;
        }

        PlayerInput.transform.SetParent(target.transform);
        SetInputActionMap(target._inputActionMap.ToString());

        target.enabled = true;

        enabled = false;
    }
    
    private void SwitchInputContext(InputAction.CallbackContext context)
    {
        SwitchInput();
        RemoveListeners();
    }

    protected abstract void AddListeners();

    protected abstract void RemoveListeners();

    private void OnDisable()
    {
        ControlsActions["SwitchInput"].performed -= SwitchInputContext;
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }
}
