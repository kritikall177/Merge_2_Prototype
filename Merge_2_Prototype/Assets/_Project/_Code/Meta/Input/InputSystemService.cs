using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project._Code.Meta.Input
{
    public class InputSystemService : InputSystem_Actions.IGameInputActions, IInputSystem, IInitializable, IDisposable
    {
        public event Action OnSelectEvent;
        public event Action OnTriggerEvent;
        
        private InputSystem_Actions _inputSystemActions;
        private bool _isPressed;

        public void Initialize()
        {
            _inputSystemActions =  new InputSystem_Actions();
            _inputSystemActions.GameInput.SetCallbacks(this);
            _inputSystemActions.GameInput.Enable();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            if (context.performed && !_isPressed) OnSelectEvent?.Invoke();
        }

        public void OnTrigger(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isPressed = true;
                OnTriggerEvent?.Invoke();
            }

            if (context.canceled)
            {
                _isPressed = false;
            }
        }

        public void Dispose()
        {
            _inputSystemActions.GameInput.Disable();
        }
    }
}