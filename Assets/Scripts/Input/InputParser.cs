using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputParser : MonoBehaviour
{
    protected InputActionAsset ControlsActions;
    protected PlayerInput PlayerInput;
    protected InputActionMap CurrentActionMap => PlayerInput.currentActionMap;
    
    protected void Start()
    {
        InitInput();

        AddListeners();

        TrailingStart();
    }

    protected abstract void TrailingStart();
    
    private void InitInput()
    {
        PlayerInput = GetComponent<PlayerInput>();
        ControlsActions = PlayerInput.actions;

        ControlsActions.Enable();
    }
    
    public void SetInputActionMap(string inputType)
    {
        PlayerInput.currentActionMap = ControlsActions.FindActionMap(inputType);
    }

    public void SwitchInput(GameObject target, string inputType)
    {
        
    }

    protected abstract void AddListeners();

    protected abstract void RemoveListeners();

    protected void RemoveAllListeners()
    {
        RemoveListeners();
    }

    private void OnDestroy()
    {
        RemoveAllListeners();
    }
}
