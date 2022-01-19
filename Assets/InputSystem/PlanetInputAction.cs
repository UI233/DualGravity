// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlanetInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlanetInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlanetInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlanetInputAction"",
    ""maps"": [
        {
            ""name"": ""GravityControl"",
            ""id"": ""1d4293d1-c4a2-4d26-b5aa-c09dae0141de"",
            ""actions"": [
                {
                    ""name"": ""PlanetA"",
                    ""type"": ""Button"",
                    ""id"": ""c372f89b-dfa6-4e7c-ab87-b853738521f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlanetB"",
                    ""type"": ""Button"",
                    ""id"": ""900e6bf8-9796-4c41-b8aa-ad20d779630a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""300ef19b-9430-4dc6-9ce5-d36d2604911e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""986306df-5601-477d-b3a6-71872eabf456"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetA"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8e8ee69f-b372-4a77-a3f3-abb47bf6c03a"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5099ea73-3fe4-419c-b5e0-bf2ae8edaf0d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""20d85898-94c0-4b12-82b9-8432aea25bb6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetA"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1481273b-c79a-445a-933e-863cd7730d93"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""270a49e3-4b90-4d3a-9220-b03536239935"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e9423d71-b5c9-44f9-91eb-ac024808a08b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""7a05bf04-4e2e-41f9-b1c9-470a1ebc5ede"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetB"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5b9ed0dd-d300-426d-b8e7-dc3e0225ef95"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""18aa3d75-cc43-4e41-a32a-3bd12b80181c"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f61bfa55-9160-497a-b232-72081abe58fc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetB"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""46645f6e-0f58-4eec-af25-55ed5c81a51c"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e6ed1ddf-a380-436b-9a99-a9f95182005b"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlanetB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GravityControl
        m_GravityControl = asset.FindActionMap("GravityControl", throwIfNotFound: true);
        m_GravityControl_PlanetA = m_GravityControl.FindAction("PlanetA", throwIfNotFound: true);
        m_GravityControl_PlanetB = m_GravityControl.FindAction("PlanetB", throwIfNotFound: true);
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

    // GravityControl
    private readonly InputActionMap m_GravityControl;
    private IGravityControlActions m_GravityControlActionsCallbackInterface;
    private readonly InputAction m_GravityControl_PlanetA;
    private readonly InputAction m_GravityControl_PlanetB;
    public struct GravityControlActions
    {
        private @PlanetInputAction m_Wrapper;
        public GravityControlActions(@PlanetInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlanetA => m_Wrapper.m_GravityControl_PlanetA;
        public InputAction @PlanetB => m_Wrapper.m_GravityControl_PlanetB;
        public InputActionMap Get() { return m_Wrapper.m_GravityControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GravityControlActions set) { return set.Get(); }
        public void SetCallbacks(IGravityControlActions instance)
        {
            if (m_Wrapper.m_GravityControlActionsCallbackInterface != null)
            {
                @PlanetA.started -= m_Wrapper.m_GravityControlActionsCallbackInterface.OnPlanetA;
                @PlanetA.performed -= m_Wrapper.m_GravityControlActionsCallbackInterface.OnPlanetA;
                @PlanetA.canceled -= m_Wrapper.m_GravityControlActionsCallbackInterface.OnPlanetA;
                @PlanetB.started -= m_Wrapper.m_GravityControlActionsCallbackInterface.OnPlanetB;
                @PlanetB.performed -= m_Wrapper.m_GravityControlActionsCallbackInterface.OnPlanetB;
                @PlanetB.canceled -= m_Wrapper.m_GravityControlActionsCallbackInterface.OnPlanetB;
            }
            m_Wrapper.m_GravityControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlanetA.started += instance.OnPlanetA;
                @PlanetA.performed += instance.OnPlanetA;
                @PlanetA.canceled += instance.OnPlanetA;
                @PlanetB.started += instance.OnPlanetB;
                @PlanetB.performed += instance.OnPlanetB;
                @PlanetB.canceled += instance.OnPlanetB;
            }
        }
    }
    public GravityControlActions @GravityControl => new GravityControlActions(this);
    public interface IGravityControlActions
    {
        void OnPlanetA(InputAction.CallbackContext context);
        void OnPlanetB(InputAction.CallbackContext context);
    }
}
