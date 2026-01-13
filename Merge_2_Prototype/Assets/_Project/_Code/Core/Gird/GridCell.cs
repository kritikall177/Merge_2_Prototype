using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gird
{
    public class GridCell : MonoBehaviour, IGridCell
    {
        public GridPosition GridPosition { get; private set; }
        
        [SerializeField] private SpriteRenderer _sprite;
        
        //вынести в конфиг
        private Color _defaultColor = Color.white;
        private Color _selectedColor = Color.chartreuse;
        private ISpawner _spawner;
        
        public void Initialize(GridPosition gridPosition, float gridScale, ISpawner spawner = null)
        {
            GridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.localScale = new Vector3(gridScale, gridScale, gridScale);
            _spawner = spawner;
            _sprite.color = _defaultColor;
        }

        public void TriggerGridCell()
        {
            _spawner?.Activate();
        }
        public void SelectGridCell()
        {
            _sprite.color = _selectedColor;
        }

        public void UnselectGridCell()
        {
            _sprite.color = _defaultColor;
        }

        public void AddSpawner(ISpawner spawner) => _spawner = spawner;

        public void DeleteSpawner() => _spawner = null;

        public ISpawner GetSpawner() => _spawner;
        
        public class Factory : PlaceholderFactory<GridCell>
        {
            
        }
    }
}