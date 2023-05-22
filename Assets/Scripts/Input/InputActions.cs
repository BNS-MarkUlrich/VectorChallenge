//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/Input/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""RTSControls"",
            ""id"": ""db75abd6-095e-4da9-9eab-318f29c4b2b1"",
            ""actions"": [
                {
                    ""name"": ""SetShipDestination"",
                    ""type"": ""Button"",
                    ""id"": ""019a350c-eccc-4cf0-83b6-4686af9e0ff6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""1cb276e6-688e-4ee2-b40d-7f51df560a9b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""InputMovement"",
                    ""type"": ""Value"",
                    ""id"": ""c7a95768-338f-4caa-8d67-2bc6ae811f3a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99008916-8c03-4d57-bb6f-a523a6fb9447"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RTS"",
                    ""action"": ""SetShipDestination"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6bc7334-b9eb-4ece-b2b4-1c20df2470aa"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RTS"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""289c669d-5d9c-400d-b2da-3ee697812034"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d6e4b408-6bd3-49f3-96a0-96a1918c3088"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4d0843be-a1fe-49ea-91c8-7e2077851f2d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b1c3ffc6-abb2-4501-b57f-56c313a6327a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""edb2825a-b0ed-4137-9aea-d55c1a290da4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""RTS"",
            ""bindingGroup"": ""RTS"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // RTSControls
        m_RTSControls = asset.FindActionMap("RTSControls", throwIfNotFound: true);
        m_RTSControls_SetShipDestination = m_RTSControls.FindAction("SetShipDestination", throwIfNotFound: true);
        m_RTSControls_MousePosition = m_RTSControls.FindAction("MousePosition", throwIfNotFound: true);
        m_RTSControls_InputMovement = m_RTSControls.FindAction("InputMovement", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // RTSControls
    private readonly InputActionMap m_RTSControls;
    private List<IRTSControlsActions> m_RTSControlsActionsCallbackInterfaces = new List<IRTSControlsActions>();
    private readonly InputAction m_RTSControls_SetShipDestination;
    private readonly InputAction m_RTSControls_MousePosition;
    private readonly InputAction m_RTSControls_InputMovement;
    public struct RTSControlsActions
    {
        private @InputActions m_Wrapper;
        public RTSControlsActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SetShipDestination => m_Wrapper.m_RTSControls_SetShipDestination;
        public InputAction @MousePosition => m_Wrapper.m_RTSControls_MousePosition;
        public InputAction @InputMovement => m_Wrapper.m_RTSControls_InputMovement;
        public InputActionMap Get() { return m_Wrapper.m_RTSControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RTSControlsActions set) { return set.Get(); }
        public void AddCallbacks(IRTSControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_RTSControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RTSControlsActionsCallbackInterfaces.Add(instance);
            @SetShipDestination.started += instance.OnSetShipDestination;
            @SetShipDestination.performed += instance.OnSetShipDestination;
            @SetShipDestination.canceled += instance.OnSetShipDestination;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @InputMovement.started += instance.OnInputMovement;
            @InputMovement.performed += instance.OnInputMovement;
            @InputMovement.canceled += instance.OnInputMovement;
        }

        private void UnregisterCallbacks(IRTSControlsActions instance)
        {
            @SetShipDestination.started -= instance.OnSetShipDestination;
            @SetShipDestination.performed -= instance.OnSetShipDestination;
            @SetShipDestination.canceled -= instance.OnSetShipDestination;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @InputMovement.started -= instance.OnInputMovement;
            @InputMovement.performed -= instance.OnInputMovement;
            @InputMovement.canceled -= instance.OnInputMovement;
        }

        public void RemoveCallbacks(IRTSControlsActions instance)
        {
            if (m_Wrapper.m_RTSControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRTSControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_RTSControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RTSControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RTSControlsActions @RTSControls => new RTSControlsActions(this);
    private int m_RTSSchemeIndex = -1;
    public InputControlScheme RTSScheme
    {
        get
        {
            if (m_RTSSchemeIndex == -1) m_RTSSchemeIndex = asset.FindControlSchemeIndex("RTS");
            return asset.controlSchemes[m_RTSSchemeIndex];
        }
    }
    public interface IRTSControlsActions
    {
        void OnSetShipDestination(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnInputMovement(InputAction.CallbackContext context);
    }
}
