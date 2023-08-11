using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public abstract class InputParser : MonoBehaviour
{
    [SerializeField] protected InputTypes _inputActionMap;
    [SerializeField] protected Camera _myCamera;
    protected InputActionAsset ControlsActions;
    
    [Header("Cursor")]
    [SerializeField] protected float _mouseSensitivity = 5;
    [SerializeField] private bool _cursorVisibility = true;
    [SerializeField] private CursorLockMode _cursorLockMode;

    protected bool HasListeners;
    protected PlayerInput PlayerInput;
    protected InputActionMap CurrentActionMap => PlayerInput.currentActionMap;

    protected virtual void OnEnable()
    {
        InitInput();

        AddListeners(out HasListeners);
    }

    protected virtual void OnDisable()
    {
        RemoveListeners();
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void InitInput()
    {
        PlayerInput = GetComponentInChildren<PlayerInput>();
        ControlsActions = PlayerInput.actions;

        ControlsActions.Enable();
        
        Cursor.lockState = _cursorLockMode;
        Cursor.visible = _cursorVisibility;
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
    
    protected Vector2 GetMouseDelta()
    {
        return ControlsActions["MouseDelta"].ReadValue<Vector2>() * (_mouseSensitivity * Time.deltaTime);
    }

    public void DestroyInputHandler()
    {
        Destroy(PlayerInput.gameObject);
    }

    protected abstract void AddListeners(out bool hasListeners);

    protected abstract void RemoveListeners();
}
