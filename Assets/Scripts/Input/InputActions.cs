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
            ""name"": ""RTS"",
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
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b36924a3-84b4-4676-9131-768607e70dfe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ActivateRotation"",
                    ""type"": ""Button"",
                    ""id"": ""e8cdce70-d667-4abf-bf4a-38b5bcc58c24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1.401298E-45)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7318003e-9cc9-4490-a1be-d537ae0148a7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollZoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d5a19152-6d63-4697-9992-82a8448cc167"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FocusOnTarget"",
                    ""type"": ""Button"",
                    ""id"": ""6081ba4d-c84b-45f8-8758-46befe73d6e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchInput"",
                    ""type"": ""Button"",
                    ""id"": ""fcbd4894-6600-4206-84d8-5f32477b47c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99008916-8c03-4d57-bb6f-a523a6fb9447"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
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
                    ""groups"": ""PC"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""418c8804-b04e-4962-b596-fbb46db26e2a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5c5edae0-4494-4819-9b1b-75234a02b6e4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""885def62-bb65-4bc9-b755-7f2d8259b81a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f553733c-782c-431c-b480-f53ac190b2b6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4660f7ba-1499-4f35-925f-4b734ed4419f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""adc440a2-fd27-4998-8ee8-1246dd17d01c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""ActivateRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7bdacc2-d6a8-4753-aa98-77c5a10e41bd"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b43b00e2-6f16-4614-97ba-ce70b08e0fcf"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""ScrollZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba33fbc3-5ff0-4216-9f85-879960fc1f5a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""FocusOnTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7290249a-cc10-490a-aedb-792032f0da4a"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""SwitchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""5a584609-9184-4244-8365-c47eb7ea4bcf"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ace47d6b-af71-419a-95a3-22daf81fb1be"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SwitchInput"",
                    ""type"": ""Button"",
                    ""id"": ""ce9eb22f-f74c-4ec9-9e39-5cc21a0453a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LookRotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fcb4f7b2-3fc4-43ce-94a7-19f878b470e9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""426393f0-ed69-4cc9-8e4d-7742c3206fe0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5bc9279c-5243-4b14-a016-6be4b0701e11"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7cb13459-a925-49f7-a40a-6e5be615ef8c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2fee8dfa-0dec-4be7-9289-bbe283ccf3b0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""452905c4-5b0f-4a16-8068-586a407ac541"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6a0f5a87-ed11-4684-89cc-085dda00f6f0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""56fea09f-63dc-43e1-adf3-5cf67ec5bfce"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""SwitchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c487305d-f271-4049-a4a5-63fda268bdb8"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""LookRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0ca5210-cce3-40b0-b2e2-7c7b3b3b4c6f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Ship"",
            ""id"": ""10f8b3c2-c014-459d-975b-c8fd39a571df"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""4d481d96-678d-40f2-bd58-7460f292e8a6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""434a54ab-2958-4028-ac95-83f4376f4878"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c1b554fd-a609-4123-bf54-9c7e357e5246"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""09df635c-fc9c-496c-97ab-492972dc35f4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""61ec10b5-8f03-429e-aec7-4be6bfbcd901"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""54fbeb62-1151-4056-8552-cbe0055cce6b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
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
        // RTS
        m_RTS = asset.FindActionMap("RTS", throwIfNotFound: true);
        m_RTS_SetShipDestination = m_RTS.FindAction("SetShipDestination", throwIfNotFound: true);
        m_RTS_MousePosition = m_RTS.FindAction("MousePosition", throwIfNotFound: true);
        m_RTS_Movement = m_RTS.FindAction("Movement", throwIfNotFound: true);
        m_RTS_ActivateRotation = m_RTS.FindAction("ActivateRotation", throwIfNotFound: true);
        m_RTS_MouseDelta = m_RTS.FindAction("MouseDelta", throwIfNotFound: true);
        m_RTS_ScrollZoom = m_RTS.FindAction("ScrollZoom", throwIfNotFound: true);
        m_RTS_FocusOnTarget = m_RTS.FindAction("FocusOnTarget", throwIfNotFound: true);
        m_RTS_SwitchInput = m_RTS.FindAction("SwitchInput", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_SwitchInput = m_Player.FindAction("SwitchInput", throwIfNotFound: true);
        m_Player_LookRotation = m_Player.FindAction("LookRotation", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        // Ship
        m_Ship = asset.FindActionMap("Ship", throwIfNotFound: true);
        m_Ship_Movement = m_Ship.FindAction("Movement", throwIfNotFound: true);
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

    // RTS
    private readonly InputActionMap m_RTS;
    private List<IRTSActions> m_RTSActionsCallbackInterfaces = new List<IRTSActions>();
    private readonly InputAction m_RTS_SetShipDestination;
    private readonly InputAction m_RTS_MousePosition;
    private readonly InputAction m_RTS_Movement;
    private readonly InputAction m_RTS_ActivateRotation;
    private readonly InputAction m_RTS_MouseDelta;
    private readonly InputAction m_RTS_ScrollZoom;
    private readonly InputAction m_RTS_FocusOnTarget;
    private readonly InputAction m_RTS_SwitchInput;
    public struct RTSActions
    {
        private @InputActions m_Wrapper;
        public RTSActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SetShipDestination => m_Wrapper.m_RTS_SetShipDestination;
        public InputAction @MousePosition => m_Wrapper.m_RTS_MousePosition;
        public InputAction @Movement => m_Wrapper.m_RTS_Movement;
        public InputAction @ActivateRotation => m_Wrapper.m_RTS_ActivateRotation;
        public InputAction @MouseDelta => m_Wrapper.m_RTS_MouseDelta;
        public InputAction @ScrollZoom => m_Wrapper.m_RTS_ScrollZoom;
        public InputAction @FocusOnTarget => m_Wrapper.m_RTS_FocusOnTarget;
        public InputAction @SwitchInput => m_Wrapper.m_RTS_SwitchInput;
        public InputActionMap Get() { return m_Wrapper.m_RTS; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RTSActions set) { return set.Get(); }
        public void AddCallbacks(IRTSActions instance)
        {
            if (instance == null || m_Wrapper.m_RTSActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RTSActionsCallbackInterfaces.Add(instance);
            @SetShipDestination.started += instance.OnSetShipDestination;
            @SetShipDestination.performed += instance.OnSetShipDestination;
            @SetShipDestination.canceled += instance.OnSetShipDestination;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @ActivateRotation.started += instance.OnActivateRotation;
            @ActivateRotation.performed += instance.OnActivateRotation;
            @ActivateRotation.canceled += instance.OnActivateRotation;
            @MouseDelta.started += instance.OnMouseDelta;
            @MouseDelta.performed += instance.OnMouseDelta;
            @MouseDelta.canceled += instance.OnMouseDelta;
            @ScrollZoom.started += instance.OnScrollZoom;
            @ScrollZoom.performed += instance.OnScrollZoom;
            @ScrollZoom.canceled += instance.OnScrollZoom;
            @FocusOnTarget.started += instance.OnFocusOnTarget;
            @FocusOnTarget.performed += instance.OnFocusOnTarget;
            @FocusOnTarget.canceled += instance.OnFocusOnTarget;
            @SwitchInput.started += instance.OnSwitchInput;
            @SwitchInput.performed += instance.OnSwitchInput;
            @SwitchInput.canceled += instance.OnSwitchInput;
        }

        private void UnregisterCallbacks(IRTSActions instance)
        {
            @SetShipDestination.started -= instance.OnSetShipDestination;
            @SetShipDestination.performed -= instance.OnSetShipDestination;
            @SetShipDestination.canceled -= instance.OnSetShipDestination;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @ActivateRotation.started -= instance.OnActivateRotation;
            @ActivateRotation.performed -= instance.OnActivateRotation;
            @ActivateRotation.canceled -= instance.OnActivateRotation;
            @MouseDelta.started -= instance.OnMouseDelta;
            @MouseDelta.performed -= instance.OnMouseDelta;
            @MouseDelta.canceled -= instance.OnMouseDelta;
            @ScrollZoom.started -= instance.OnScrollZoom;
            @ScrollZoom.performed -= instance.OnScrollZoom;
            @ScrollZoom.canceled -= instance.OnScrollZoom;
            @FocusOnTarget.started -= instance.OnFocusOnTarget;
            @FocusOnTarget.performed -= instance.OnFocusOnTarget;
            @FocusOnTarget.canceled -= instance.OnFocusOnTarget;
            @SwitchInput.started -= instance.OnSwitchInput;
            @SwitchInput.performed -= instance.OnSwitchInput;
            @SwitchInput.canceled -= instance.OnSwitchInput;
        }

        public void RemoveCallbacks(IRTSActions instance)
        {
            if (m_Wrapper.m_RTSActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRTSActions instance)
        {
            foreach (var item in m_Wrapper.m_RTSActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RTSActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RTSActions @RTS => new RTSActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_SwitchInput;
    private readonly InputAction m_Player_LookRotation;
    private readonly InputAction m_Player_Interact;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @SwitchInput => m_Wrapper.m_Player_SwitchInput;
        public InputAction @LookRotation => m_Wrapper.m_Player_LookRotation;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @SwitchInput.started += instance.OnSwitchInput;
            @SwitchInput.performed += instance.OnSwitchInput;
            @SwitchInput.canceled += instance.OnSwitchInput;
            @LookRotation.started += instance.OnLookRotation;
            @LookRotation.performed += instance.OnLookRotation;
            @LookRotation.canceled += instance.OnLookRotation;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @SwitchInput.started -= instance.OnSwitchInput;
            @SwitchInput.performed -= instance.OnSwitchInput;
            @SwitchInput.canceled -= instance.OnSwitchInput;
            @LookRotation.started -= instance.OnLookRotation;
            @LookRotation.performed -= instance.OnLookRotation;
            @LookRotation.canceled -= instance.OnLookRotation;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Ship
    private readonly InputActionMap m_Ship;
    private List<IShipActions> m_ShipActionsCallbackInterfaces = new List<IShipActions>();
    private readonly InputAction m_Ship_Movement;
    public struct ShipActions
    {
        private @InputActions m_Wrapper;
        public ShipActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Ship_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Ship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
        public void AddCallbacks(IShipActions instance)
        {
            if (instance == null || m_Wrapper.m_ShipActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ShipActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
        }

        private void UnregisterCallbacks(IShipActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
        }

        public void RemoveCallbacks(IShipActions instance)
        {
            if (m_Wrapper.m_ShipActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IShipActions instance)
        {
            foreach (var item in m_Wrapper.m_ShipActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ShipActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ShipActions @Ship => new ShipActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IRTSActions
    {
        void OnSetShipDestination(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnActivateRotation(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnScrollZoom(InputAction.CallbackContext context);
        void OnFocusOnTarget(InputAction.CallbackContext context);
        void OnSwitchInput(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSwitchInput(InputAction.CallbackContext context);
        void OnLookRotation(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IShipActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
