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
        private IGridSystem _gridSystem;
        
        private Camera _camera;
        private IGridCell _selectedGridCell = null;

        public TriggerRayEmitter(IInputSystem  inputSystem,  IGridSystem gridSystem)
        {
            _inputSystem = inputSystem;
            _gridSystem = gridSystem;
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
            _selectedGridCell = RayHit();
            _selectedGridCell?.SelectGridCell();
        }
        
        private void OnTriggerEvent()
        {
            IGridCell gridCell = RayHit();

            if (gridCell != null)
            {
                if (_selectedGridCell  != null && _selectedGridCell != gridCell)
                {
                    _gridSystem.TryMergeGridPosition(_selectedGridCell.GridPosition, gridCell.GridPosition);
                    _selectedGridCell.UnselectGridCell();
                    _selectedGridCell =  null;
                    return;
                }

                if (_selectedGridCell == null)
                {
                    gridCell.TriggerGridCell();
                }
            }
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