using System;
using _Project._Code.Core.Gird;
using _Project._Code.Meta.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project._Code
{
    public class TriggerRayEmitter : IInitializable, IDisposable
    {
        private IInputSystem _inputSystem;
        private Camera _camera;

        public TriggerRayEmitter(IInputSystem  inputSystem)
        {
            _inputSystem = inputSystem;
        }
        
        public void Initialize()
        {
            _inputSystem.OnSelectEvent += OnSelectEvent;
            _inputSystem.OnTriggerEvent += OnTriggerEvent;
            
            _camera =  Camera.main;
        }

        public void Dispose()
        {
            _inputSystem.OnSelectEvent -= OnSelectEvent;
            _inputSystem.OnTriggerEvent -= OnTriggerEvent;
        }

        private void OnSelectEvent()
        {
            RayHit()?.SelectGridObject();
        }
        
        private void OnTriggerEvent()
        {
            RayHit()?.TriggerGridCell();
        }
        

        private IGridCell RayHit()
        {
            Vector2 cursorPosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            RaycastHit2D hit = Physics2D.Raycast(cursorPosition, Vector2.zero);
            
            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<IGridCell>(out var gridCell))
            {
                return gridCell;
            }
            
            return null;
        }
        
    }
}