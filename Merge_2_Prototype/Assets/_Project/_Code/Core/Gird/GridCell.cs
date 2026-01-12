using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gird
{
    public class GridCell : MonoBehaviour, IGridCell
    {
        private IGridObject _gridObject;
        
        public void Initialize(GridPosition gridPosition, float gridScale, IGridObject gridObject = null)
        {
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.localScale = new Vector3(gridScale, gridScale, gridScale);
            _gridObject = gridObject;
        }

        public void TriggerGridCell()
        {
            _gridObject?.Activate();
        }
        public void SelectGridObject()
        {
            
        }

        public void AddGridObject(IGridObject gridObject) => _gridObject = gridObject;

        public void DeleteGridObject() => _gridObject = null;

        public IGridObject GetGridObject() => _gridObject;
        
        public class Factory : PlaceholderFactory<GridCell>
        {
            
        }
    }
}