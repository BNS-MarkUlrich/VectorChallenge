using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputParser : MonoBehaviour
{
    [SerializeField] protected InputTypes _inputActionMap;
    [SerializeField] protected Camera _myCamera;
    protected InputActionAsset ControlsActions;

    protected bool HasListeners;
    protected PlayerInput PlayerInput;
    protected InputActionMap CurrentActionMap => PlayerInput.currentActionMap;

    private void OnEnable()
    {
        InitInput();

        AddListeners(out HasListeners);
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

    public void SwitchInput(InputParser target)
    {
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

    protected abstract void AddListeners(out bool hasListeners);

    protected abstract void RemoveListeners();
}
