using System;
using Cysharp.Threading.Tasks;
using IsolarvHelperTools.Runtime;
using PanzerHero.Runtime.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PanzerHero.Runtime.Systems
{
    public class PlayerInputSystem : UnitySingleton<PlayerInputSystem>
    {
        PlayerInputActions playerInputActions;
        
        PlayerInputActions.PlayerActions playerAction;
        PlayerInputActions.UIActions uiAction;

        void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerAction = playerInputActions.Player;
            uiAction = playerInputActions.UI;
        }

        private void OnEnable()
        {
            SubcribeEvents();
        }
        
        private void OnDisable()
        {
            UnsubcribeEvents();
        }
        
        void SubcribeEvents()
        {
            playerInputActions.Enable();
            
            playerAction.Move.performed += SetMotor_Internal;
            playerAction.Fire.performed += Fire_Internal;
            playerAction.Zoom.performed += ChangeZoom_Internal;
            
            uiAction.Escape.performed += Escape_Internal;
        }
        
        void UnsubcribeEvents()
        {
            playerInputActions.Disable();
            
            playerAction.Move.performed -= SetMotor_Internal;
            playerAction.Fire.performed -= Fire_Internal;
            playerAction.Zoom.performed -= ChangeZoom_Internal;
            
            uiAction.Escape.performed -= Escape_Internal;
        }
        
        public event Action<int> OnMotorInput;
        public event Action<Vector3> OnFireInput;
        public event Action OnZoomInput;

        public event Action OnEscapeInput;
        
        void SetMotor_Internal(InputAction.CallbackContext context)
        {
            var integer = context.ReadValue<int>();
            OnMotorInput?.Invoke(integer);
        }
        
        void Fire_Internal(InputAction.CallbackContext context)
        {
            var vector2 = context.ReadValue<Vector2>();
            if (PointerExtensions.IsPointerOverUIObject(vector2))
                return;

            var worldPoint = PointerExtensions.ConvertScreenInputToWorldPosition(vector2);
            OnFireInput?.Invoke(worldPoint);
        }

        void ChangeZoom_Internal(InputAction.CallbackContext context)
        {
            OnZoomInput?.Invoke();
        }
        
        void Escape_Internal(InputAction.CallbackContext context)
        {
            OnEscapeInput?.Invoke();
        }
    }
}