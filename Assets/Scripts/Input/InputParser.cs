using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public abstract class InputParser : MonoBehaviour
{
    [SerializeField] protected InputParser _targetInputObject;
    [SerializeField] protected InputTypes _inputActionMap;
    [SerializeField] protected Camera _myCamera;
    protected InputActionAsset ControlsActions;
    protected PlayerInput PlayerInput;
    protected InputActionMap CurrentActionMap => PlayerInput.currentActionMap;

    private void OnEnable()
    {
        InitInput();

        AddListeners();
        ControlsActions["SwitchInput"].performed += SwitchInputContext;
    }

    private void OnDisable()
    {
        ControlsActions["SwitchInput"].performed -= SwitchInputContext;
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }

    private void InitInput()
    {
        PlayerInput = GetComponentInChildren<PlayerInput>();
        ControlsActions = PlayerInput.actions;

        ControlsActions.Enable();
    }

    private void SetInputActionMap(string inputType)
    {
        PlayerInput.currentActionMap = ControlsActions.FindActionMap(inputType);
    }

    public void SwitchInput(InputParser target = null)
    {
        if (target == null)
        {
            target = _targetInputObject;
        }

        PlayerInput.transform.SetParent(target.transform);
        SetInputActionMap(target._inputActionMap.ToString());

        target.enabled = true;
        
        _myCamera.gameObject.SetActive(false);

        var targetCamera = target._myCamera;
        targetCamera.gameObject.SetActive(true);
        Camera.SetupCurrent(targetCamera);
        
        enabled = false;
    }

    private void SwitchInputContext(InputAction.CallbackContext context)
    {
        SwitchInput();
        RemoveListeners();
    }

    protected abstract void AddListeners();

    protected abstract void RemoveListeners();
}
