//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/PlayerController.inputactions
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

public partial class @PlayerController: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""PC"",
            ""id"": ""747b1c94-6326-4182-92fc-6a1614c59219"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""bf684872-f707-4daf-a7b3-cf79f7cf0a80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""724cf241-b15f-4311-97ce-c564b863e2b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Defense"",
                    ""type"": ""Button"",
                    ""id"": ""ecc699ef-4a44-493c-8d24-75883d92e191"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AttackSkill"",
                    ""type"": ""Button"",
                    ""id"": ""210e4cef-219e-4444-982a-17b3c1728806"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""JumpSkill"",
                    ""type"": ""Button"",
                    ""id"": ""dfd2a853-56db-476e-8c47-b88f62d09b56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""7364c57b-8935-4d08-ae9e-ab5b50eabe35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6990e015-4ca2-42b1-adf4-b0ebfaba532f"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c949d52-dff1-49de-be42-33a5798dfe49"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b20c8469-a52e-4178-957a-62249c8fd044"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Defense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c389059-7e8c-4050-8871-e8f7685fa15f"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""AttackSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8454ab76-87b3-4003-a995-8f1e8300faa8"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""JumpSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""188b2809-2018-44fe-a817-22302423981c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""id"": ""c88dddb9-e22f-4822-9ca8-07217c963e26"",
            ""actions"": [
                {
                    ""name"": ""Touch"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8df1c906-839f-4c17-ab50-4c071cefe77c"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CancelTouch"",
                    ""type"": ""Button"",
                    ""id"": ""95a32c79-ccae-4297-aa9e-a278ad3b60e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1de6d419-2411-41e7-b3b4-0fc1f0d6339b"",
                    ""path"": ""<Touchscreen>/touch0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Moblie"",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""536a79f1-e18c-4ae7-bda0-ed58efce78b5"",
                    ""path"": ""<Touchscreen>/touch1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Moblie"",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c0e75d2-1874-4b53-9c9a-71de7738d77a"",
                    ""path"": ""<Touchscreen>/touch2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Moblie"",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b13a8fac-10c5-4717-9bb9-bca23f54cbea"",
                    ""path"": ""*/{Back}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Moblie"",
                    ""action"": ""CancelTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f467847-b91f-476e-95d2-83f6a64ef19a"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Moblie"",
                    ""action"": ""CancelTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Moblie"",
            ""bindingGroup"": ""Moblie"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PC
        m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
        m_PC_Attack = m_PC.FindAction("Attack", throwIfNotFound: true);
        m_PC_Jump = m_PC.FindAction("Jump", throwIfNotFound: true);
        m_PC_Defense = m_PC.FindAction("Defense", throwIfNotFound: true);
        m_PC_AttackSkill = m_PC.FindAction("AttackSkill", throwIfNotFound: true);
        m_PC_JumpSkill = m_PC.FindAction("JumpSkill", throwIfNotFound: true);
        m_PC_Cancel = m_PC.FindAction("Cancel", throwIfNotFound: true);
        // Mobile
        m_Mobile = asset.FindActionMap("Mobile", throwIfNotFound: true);
        m_Mobile_Touch = m_Mobile.FindAction("Touch", throwIfNotFound: true);
        m_Mobile_CancelTouch = m_Mobile.FindAction("CancelTouch", throwIfNotFound: true);
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

    // PC
    private readonly InputActionMap m_PC;
    private List<IPCActions> m_PCActionsCallbackInterfaces = new List<IPCActions>();
    private readonly InputAction m_PC_Attack;
    private readonly InputAction m_PC_Jump;
    private readonly InputAction m_PC_Defense;
    private readonly InputAction m_PC_AttackSkill;
    private readonly InputAction m_PC_JumpSkill;
    private readonly InputAction m_PC_Cancel;
    public struct PCActions
    {
        private @PlayerController m_Wrapper;
        public PCActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_PC_Attack;
        public InputAction @Jump => m_Wrapper.m_PC_Jump;
        public InputAction @Defense => m_Wrapper.m_PC_Defense;
        public InputAction @AttackSkill => m_Wrapper.m_PC_AttackSkill;
        public InputAction @JumpSkill => m_Wrapper.m_PC_JumpSkill;
        public InputAction @Cancel => m_Wrapper.m_PC_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void AddCallbacks(IPCActions instance)
        {
            if (instance == null || m_Wrapper.m_PCActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCActionsCallbackInterfaces.Add(instance);
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Defense.started += instance.OnDefense;
            @Defense.performed += instance.OnDefense;
            @Defense.canceled += instance.OnDefense;
            @AttackSkill.started += instance.OnAttackSkill;
            @AttackSkill.performed += instance.OnAttackSkill;
            @AttackSkill.canceled += instance.OnAttackSkill;
            @JumpSkill.started += instance.OnJumpSkill;
            @JumpSkill.performed += instance.OnJumpSkill;
            @JumpSkill.canceled += instance.OnJumpSkill;
            @Cancel.started += instance.OnCancel;
            @Cancel.performed += instance.OnCancel;
            @Cancel.canceled += instance.OnCancel;
        }

        private void UnregisterCallbacks(IPCActions instance)
        {
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Defense.started -= instance.OnDefense;
            @Defense.performed -= instance.OnDefense;
            @Defense.canceled -= instance.OnDefense;
            @AttackSkill.started -= instance.OnAttackSkill;
            @AttackSkill.performed -= instance.OnAttackSkill;
            @AttackSkill.canceled -= instance.OnAttackSkill;
            @JumpSkill.started -= instance.OnJumpSkill;
            @JumpSkill.performed -= instance.OnJumpSkill;
            @JumpSkill.canceled -= instance.OnJumpSkill;
            @Cancel.started -= instance.OnCancel;
            @Cancel.performed -= instance.OnCancel;
            @Cancel.canceled -= instance.OnCancel;
        }

        public void RemoveCallbacks(IPCActions instance)
        {
            if (m_Wrapper.m_PCActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPCActions instance)
        {
            foreach (var item in m_Wrapper.m_PCActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PCActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PCActions @PC => new PCActions(this);

    // Mobile
    private readonly InputActionMap m_Mobile;
    private List<IMobileActions> m_MobileActionsCallbackInterfaces = new List<IMobileActions>();
    private readonly InputAction m_Mobile_Touch;
    private readonly InputAction m_Mobile_CancelTouch;
    public struct MobileActions
    {
        private @PlayerController m_Wrapper;
        public MobileActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Touch => m_Wrapper.m_Mobile_Touch;
        public InputAction @CancelTouch => m_Wrapper.m_Mobile_CancelTouch;
        public InputActionMap Get() { return m_Wrapper.m_Mobile; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MobileActions set) { return set.Get(); }
        public void AddCallbacks(IMobileActions instance)
        {
            if (instance == null || m_Wrapper.m_MobileActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MobileActionsCallbackInterfaces.Add(instance);
            @Touch.started += instance.OnTouch;
            @Touch.performed += instance.OnTouch;
            @Touch.canceled += instance.OnTouch;
            @CancelTouch.started += instance.OnCancelTouch;
            @CancelTouch.performed += instance.OnCancelTouch;
            @CancelTouch.canceled += instance.OnCancelTouch;
        }

        private void UnregisterCallbacks(IMobileActions instance)
        {
            @Touch.started -= instance.OnTouch;
            @Touch.performed -= instance.OnTouch;
            @Touch.canceled -= instance.OnTouch;
            @CancelTouch.started -= instance.OnCancelTouch;
            @CancelTouch.performed -= instance.OnCancelTouch;
            @CancelTouch.canceled -= instance.OnCancelTouch;
        }

        public void RemoveCallbacks(IMobileActions instance)
        {
            if (m_Wrapper.m_MobileActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMobileActions instance)
        {
            foreach (var item in m_Wrapper.m_MobileActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MobileActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MobileActions @Mobile => new MobileActions(this);
    private int m_MoblieSchemeIndex = -1;
    public InputControlScheme MoblieScheme
    {
        get
        {
            if (m_MoblieSchemeIndex == -1) m_MoblieSchemeIndex = asset.FindControlSchemeIndex("Moblie");
            return asset.controlSchemes[m_MoblieSchemeIndex];
        }
    }
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IPCActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDefense(InputAction.CallbackContext context);
        void OnAttackSkill(InputAction.CallbackContext context);
        void OnJumpSkill(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IMobileActions
    {
        void OnTouch(InputAction.CallbackContext context);
        void OnCancelTouch(InputAction.CallbackContext context);
    }
}
