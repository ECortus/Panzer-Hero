using System;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.Input;
using PanzerHero.Runtime.LevelDesign;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PanzerHero.Runtime.Systems
{
    public class PlayerInputEvents : UnitySingleton<PlayerInputEvents>
    {
        PlayerInputActions playerInputActions;
        
        PlayerInputActions.PlayerActions playerAction;
        PlayerInputActions.UIActions uiAction;

        GameStatement statement;

        protected override void OnAwake()
        {
            statement = GameStatement.GetInstance;
            statement.OnGameStarted += SubcribeEvents;
            statement.OnGameFinished += UnsubcribeEvents;
            
            playerInputActions = new PlayerInputActions();
            playerAction = playerInputActions.Player;
            uiAction = playerInputActions.UI;

            ResetAllActions();
        }
        
        void SubcribeEvents()
        {
            playerInputActions.Enable();
            
            playerAction.Move.performed += SetMotor_Internal;
            playerAction.Move.canceled += SetMotor_Internal;
            
            playerAction.Zoom.performed += ChangeZoom_Internal;
            
            uiAction.Escape.performed += Escape_Internal;
        }
        
        void UnsubcribeEvents()
        {
            playerAction.Move.performed -= SetMotor_Internal;
            playerAction.Move.canceled -= SetMotor_Internal;
            
            playerAction.Zoom.performed -= ChangeZoom_Internal;
            
            uiAction.Escape.performed -= Escape_Internal;
            
            playerInputActions.Disable();
        }

        void Update()
        {
            FireUpdate();
        }

        void FireUpdate()
        {
            var lmbValue = playerAction.LMB.ReadValue<Vector2>();
            if (lmbValue.sqrMagnitude > 0)
            {
                FireLeftMouseClick_Internal(lmbValue);
                return;
            }

            var rmbValue = playerAction.RMB.ReadValue<Vector2>();
            if (rmbValue.sqrMagnitude > 0)
            {
                FireRightMouseClick_Internal(rmbValue);
            }
        }
        
        public event Action<Vector2> OnMotorInput;
        
        void SetMotor_Internal(InputAction.CallbackContext context)
        {
            var vector2 = context.ReadValue<Vector2>();
            OnMotorInput?.Invoke(vector2);
        }
        
        public event Action<Vector3> OnMainFireInput;

        void FireLeftMouseClick_Internal(Vector2 vector2)
        {
            var cam = Camera.main;
            if (cam.IsPointerOverUIObject(vector2))
                return;

            var layers = new [] { "Ground" };
            if (cam.ConvertScreenInputToWorldPosition(vector2, layers, out Vector3 worldPosition))
            {
                OnMainFireInput?.Invoke(worldPosition);
            }
        }
        
        public event Action<Vector3> OnAlternativeFireInput;
        
        void FireRightMouseClick_Internal(Vector2 vector2)
        {
            var cam = Camera.main;
            if (cam.IsPointerOverUIObject(vector2))
                return;

            var layers = new [] { "Ground" };
            if (cam.ConvertScreenInputToWorldPosition(vector2, layers, out Vector3 worldPosition))
            {
                OnAlternativeFireInput?.Invoke(worldPosition);
            }
        }

        public event Action OnZoomInput;
        
        void ChangeZoom_Internal(InputAction.CallbackContext context)
        {
            OnZoomInput?.Invoke();
        }
        
        public event Action OnEscapeInput;
        
        void Escape_Internal(InputAction.CallbackContext context)
        {
            OnEscapeInput?.Invoke();
        }
        
        void ResetAllActions()
        {
            OnMotorInput = null;
            OnMainFireInput = null;
            OnAlternativeFireInput = null;
            OnZoomInput = null;
            OnEscapeInput = null;
        }
    }
}