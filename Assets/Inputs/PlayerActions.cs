//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Inputs/PlayerActions.inputactions
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

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""PlayerGaming"",
            ""id"": ""3a2e3429-4548-4f48-8d1b-5df917fb004b"",
            ""actions"": [
                {
                    ""name"": ""MouseHold"",
                    ""type"": ""Button"",
                    ""id"": ""d24e7c1c-2791-4f5a-ba8d-763315db8353"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.3,pressPoint=0.5),Press(pressPoint=0.3)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseUp"",
                    ""type"": ""Button"",
                    ""id"": ""acdb073d-9b89-475f-ade1-3f490bc99742"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.25,behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseDown"",
                    ""type"": ""Button"",
                    ""id"": ""c8079be3-fd58-45af-91c2-eb0caf9f92ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.2)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ca4b942b-8c93-4d64-8f80-ed08fb0eab21"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4337f876-cc2e-4e1c-bd9c-9eeeb09ccaaa"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e2ebcf1-b442-4865-be40-e6211135d7df"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24aaac07-2c06-4e2e-9324-f462efb9d36d"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68799017-05db-41a7-abf8-ee363099461d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8476214f-3773-4184-adef-c506718702fa"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerGaming
        m_PlayerGaming = asset.FindActionMap("PlayerGaming", throwIfNotFound: true);
        m_PlayerGaming_MouseHold = m_PlayerGaming.FindAction("MouseHold", throwIfNotFound: true);
        m_PlayerGaming_MouseUp = m_PlayerGaming.FindAction("MouseUp", throwIfNotFound: true);
        m_PlayerGaming_MouseDown = m_PlayerGaming.FindAction("MouseDown", throwIfNotFound: true);
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

    // PlayerGaming
    private readonly InputActionMap m_PlayerGaming;
    private List<IPlayerGamingActions> m_PlayerGamingActionsCallbackInterfaces = new List<IPlayerGamingActions>();
    private readonly InputAction m_PlayerGaming_MouseHold;
    private readonly InputAction m_PlayerGaming_MouseUp;
    private readonly InputAction m_PlayerGaming_MouseDown;
    public struct PlayerGamingActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerGamingActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseHold => m_Wrapper.m_PlayerGaming_MouseHold;
        public InputAction @MouseUp => m_Wrapper.m_PlayerGaming_MouseUp;
        public InputAction @MouseDown => m_Wrapper.m_PlayerGaming_MouseDown;
        public InputActionMap Get() { return m_Wrapper.m_PlayerGaming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerGamingActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerGamingActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerGamingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerGamingActionsCallbackInterfaces.Add(instance);
            @MouseHold.started += instance.OnMouseHold;
            @MouseHold.performed += instance.OnMouseHold;
            @MouseHold.canceled += instance.OnMouseHold;
            @MouseUp.started += instance.OnMouseUp;
            @MouseUp.performed += instance.OnMouseUp;
            @MouseUp.canceled += instance.OnMouseUp;
            @MouseDown.started += instance.OnMouseDown;
            @MouseDown.performed += instance.OnMouseDown;
            @MouseDown.canceled += instance.OnMouseDown;
        }

        private void UnregisterCallbacks(IPlayerGamingActions instance)
        {
            @MouseHold.started -= instance.OnMouseHold;
            @MouseHold.performed -= instance.OnMouseHold;
            @MouseHold.canceled -= instance.OnMouseHold;
            @MouseUp.started -= instance.OnMouseUp;
            @MouseUp.performed -= instance.OnMouseUp;
            @MouseUp.canceled -= instance.OnMouseUp;
            @MouseDown.started -= instance.OnMouseDown;
            @MouseDown.performed -= instance.OnMouseDown;
            @MouseDown.canceled -= instance.OnMouseDown;
        }

        public void RemoveCallbacks(IPlayerGamingActions instance)
        {
            if (m_Wrapper.m_PlayerGamingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerGamingActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerGamingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerGamingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerGamingActions @PlayerGaming => new PlayerGamingActions(this);
    public interface IPlayerGamingActions
    {
        void OnMouseHold(InputAction.CallbackContext context);
        void OnMouseUp(InputAction.CallbackContext context);
        void OnMouseDown(InputAction.CallbackContext context);
    }
}
