using UnityEngine;
using UnityEngine.InputSystem;

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

        AddSwitchListener();
    }

    private void OnDisable()
    {
        RemoveSwitchListener();
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
        RemoveSwitchListener();
    }

    private void AddSwitchListener()
    {
        if (_targetInputObject != null)
        {
            ControlsActions["SwitchInput"].performed += SwitchInputContext;
        }
        else
        {
            Debug.LogError("Target Input Object is Null, cannot add listener!");
        }
    }

    private void RemoveSwitchListener()
    {
        if (ControlsActions["SwitchInput"] != null)
        {
            ControlsActions["SwitchInput"].performed -= SwitchInputContext;
        }
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

        if (target._myCamera == null)
        {
            target._myCamera = _myCamera;
            Debug.LogError("No Target Camera was set, using current camera");
        }
        else
        {
            _myCamera.gameObject.SetActive(false);
            var targetCamera = target._myCamera;
            targetCamera.gameObject.SetActive(true);
            Camera.SetupCurrent(targetCamera);
        }

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
