//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Controls.inputactions
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

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""17e7d173-3b00-4d97-8f16-a818c813cfd7"",
            ""actions"": [
                {
                    ""name"": ""LeftGrab"",
                    ""type"": ""Button"",
                    ""id"": ""9d1b6523-0c47-4f29-a7b5-3afb2bd475eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftGrab1"",
                    ""type"": ""Button"",
                    ""id"": ""1e010d8d-ad4d-4650-9204-8ad1eafc0558"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightGrab"",
                    ""type"": ""Button"",
                    ""id"": ""d37e5907-4a42-4e4d-b7e0-626408f4d529"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightGrab1"",
                    ""type"": ""Button"",
                    ""id"": ""37c9b074-153b-411f-9d52-1025aec97062"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftArm"",
                    ""type"": ""Value"",
                    ""id"": ""c49f4dae-dc5b-4a20-b5b7-9b3a53deb1e5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftArm1"",
                    ""type"": ""Value"",
                    ""id"": ""797b8ac3-41a0-4068-be51-b25169cc759b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightArm"",
                    ""type"": ""Value"",
                    ""id"": ""9682efd7-e5bd-4c1f-8c7a-8887304d4f95"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightArm1"",
                    ""type"": ""Value"",
                    ""id"": ""3bcfb2a4-56f4-4858-b9aa-a01ce3a70de4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""900a5ea0-122b-42ab-acd2-1d2f1769d567"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftGrab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d103362-9e47-416a-a197-69768143c304"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightGrab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09175f32-aab5-436f-a7ec-2ca1c0a5fbd1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db0f6d58-dfa8-418b-9652-3481b3c0c7e5"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""769001e9-6d9f-468e-a0b9-2b147618e869"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftGrab1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a60e8e9-1f7f-4b93-95dd-386853dd0107"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightGrab1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44c5f448-5b3c-4a43-8cd5-5afb8a07f8fc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArm1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f32b6261-9591-4147-a755-843a5addb16a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArm1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_LeftGrab = m_ActionMap.FindAction("LeftGrab", throwIfNotFound: true);
        m_ActionMap_LeftGrab1 = m_ActionMap.FindAction("LeftGrab1", throwIfNotFound: true);
        m_ActionMap_RightGrab = m_ActionMap.FindAction("RightGrab", throwIfNotFound: true);
        m_ActionMap_RightGrab1 = m_ActionMap.FindAction("RightGrab1", throwIfNotFound: true);
        m_ActionMap_LeftArm = m_ActionMap.FindAction("LeftArm", throwIfNotFound: true);
        m_ActionMap_LeftArm1 = m_ActionMap.FindAction("LeftArm1", throwIfNotFound: true);
        m_ActionMap_RightArm = m_ActionMap.FindAction("RightArm", throwIfNotFound: true);
        m_ActionMap_RightArm1 = m_ActionMap.FindAction("RightArm1", throwIfNotFound: true);
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

    // ActionMap
    private readonly InputActionMap m_ActionMap;
    private IActionMapActions m_ActionMapActionsCallbackInterface;
    private readonly InputAction m_ActionMap_LeftGrab;
    private readonly InputAction m_ActionMap_LeftGrab1;
    private readonly InputAction m_ActionMap_RightGrab;
    private readonly InputAction m_ActionMap_RightGrab1;
    private readonly InputAction m_ActionMap_LeftArm;
    private readonly InputAction m_ActionMap_LeftArm1;
    private readonly InputAction m_ActionMap_RightArm;
    private readonly InputAction m_ActionMap_RightArm1;
    public struct ActionMapActions
    {
        private @Controls m_Wrapper;
        public ActionMapActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftGrab => m_Wrapper.m_ActionMap_LeftGrab;
        public InputAction @LeftGrab1 => m_Wrapper.m_ActionMap_LeftGrab1;
        public InputAction @RightGrab => m_Wrapper.m_ActionMap_RightGrab;
        public InputAction @RightGrab1 => m_Wrapper.m_ActionMap_RightGrab1;
        public InputAction @LeftArm => m_Wrapper.m_ActionMap_LeftArm;
        public InputAction @LeftArm1 => m_Wrapper.m_ActionMap_LeftArm1;
        public InputAction @RightArm => m_Wrapper.m_ActionMap_RightArm;
        public InputAction @RightArm1 => m_Wrapper.m_ActionMap_RightArm1;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterface != null)
            {
                @LeftGrab.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftGrab;
                @LeftGrab.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftGrab;
                @LeftGrab.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftGrab;
                @LeftGrab1.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftGrab1;
                @LeftGrab1.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftGrab1;
                @LeftGrab1.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftGrab1;
                @RightGrab.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightGrab;
                @RightGrab.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightGrab;
                @RightGrab.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightGrab;
                @RightGrab1.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightGrab1;
                @RightGrab1.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightGrab1;
                @RightGrab1.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightGrab1;
                @LeftArm.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftArm;
                @LeftArm.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftArm;
                @LeftArm.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftArm;
                @LeftArm1.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftArm1;
                @LeftArm1.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftArm1;
                @LeftArm1.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftArm1;
                @RightArm.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightArm;
                @RightArm.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightArm;
                @RightArm.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightArm;
                @RightArm1.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightArm1;
                @RightArm1.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightArm1;
                @RightArm1.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightArm1;
            }
            m_Wrapper.m_ActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftGrab.started += instance.OnLeftGrab;
                @LeftGrab.performed += instance.OnLeftGrab;
                @LeftGrab.canceled += instance.OnLeftGrab;
                @LeftGrab1.started += instance.OnLeftGrab1;
                @LeftGrab1.performed += instance.OnLeftGrab1;
                @LeftGrab1.canceled += instance.OnLeftGrab1;
                @RightGrab.started += instance.OnRightGrab;
                @RightGrab.performed += instance.OnRightGrab;
                @RightGrab.canceled += instance.OnRightGrab;
                @RightGrab1.started += instance.OnRightGrab1;
                @RightGrab1.performed += instance.OnRightGrab1;
                @RightGrab1.canceled += instance.OnRightGrab1;
                @LeftArm.started += instance.OnLeftArm;
                @LeftArm.performed += instance.OnLeftArm;
                @LeftArm.canceled += instance.OnLeftArm;
                @LeftArm1.started += instance.OnLeftArm1;
                @LeftArm1.performed += instance.OnLeftArm1;
                @LeftArm1.canceled += instance.OnLeftArm1;
                @RightArm.started += instance.OnRightArm;
                @RightArm.performed += instance.OnRightArm;
                @RightArm.canceled += instance.OnRightArm;
                @RightArm1.started += instance.OnRightArm1;
                @RightArm1.performed += instance.OnRightArm1;
                @RightArm1.canceled += instance.OnRightArm1;
            }
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);
    public interface IActionMapActions
    {
        void OnLeftGrab(InputAction.CallbackContext context);
        void OnLeftGrab1(InputAction.CallbackContext context);
        void OnRightGrab(InputAction.CallbackContext context);
        void OnRightGrab1(InputAction.CallbackContext context);
        void OnLeftArm(InputAction.CallbackContext context);
        void OnLeftArm1(InputAction.CallbackContext context);
        void OnRightArm(InputAction.CallbackContext context);
        void OnRightArm1(InputAction.CallbackContext context);
    }
}
