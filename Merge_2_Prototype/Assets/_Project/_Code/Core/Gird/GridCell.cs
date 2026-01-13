using System.Collections.Generic;
using _Project._Code.Core.SpawnerObject;
using _Project._Code.Meta.DataConfig;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gird
{
    public class GridCell : MonoBehaviour, IGridCell
    {
        public GridPosition GridPosition { get; private set; }
        
        [SerializeField] private SpriteRenderer _sprite;

        private ISpawner _spawner;
        private Color _defaultColor;
        private Color _selectedColor;

        public void Initialize(GridPosition gridPosition, IGridConfig gridConfig, ISpawner spawner = null)
        {
            GridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.localScale = new Vector3(gridConfig.CellSize, gridConfig.CellSize, gridConfig.CellSize);
            _spawner = spawner;
            _defaultColor = gridConfig.DefaultColor;
            _selectedColor = gridConfig.SelectedColor;
            _sprite.color = _defaultColor;
        }

        public void TriggerGridCell() => _spawner?.Activate();

        public void SelectGridCell() => _sprite.color = _selectedColor;

        public void UnselectGridCell() => _sprite.color = _defaultColor;

        public void AddSpawner(ISpawner spawner) => _spawner = spawner;

        public void DeleteSpawner() => _spawner = null;

        public ISpawner GetSpawner() => _spawner;
        
        public class Factory : PlaceholderFactory<GridCell>
        {
            
        }
    }
}