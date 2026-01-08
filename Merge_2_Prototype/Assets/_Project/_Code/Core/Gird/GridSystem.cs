using _Project._Code.Meta.DataConfig;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gird
{
    public class GridSystem : IGridSystem, IInitializable
    {
        private IGridConfig _gridConfig;
        private GridCell.Factory _gridCellFactory;
        
        private GridCell[,] _gridObjectArray;
        private GameObject _gridParent;
    
        [Inject]
        public GridSystem(IGridConfig gridConfig, GridCell.Factory gridCellFactory)
        {
            _gridConfig =  gridConfig;
            _gridCellFactory = gridCellFactory;
        }

        public void Initialize()
        {
            _gridObjectArray = new GridCell[_gridConfig.Width, _gridConfig.Height];
            _gridParent = new GameObject("GameGrid");
            for (int x = 0; x < _gridConfig.Width; x++)
            {
                for (int z = 0; z < _gridConfig.Height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    _gridObjectArray[x, z] = _gridCellFactory.Create();
                    _gridObjectArray[x, z].transform.SetParent(_gridParent.transform);
                    _gridObjectArray[x, z].Initialize(gridPosition, _gridConfig.CellSize);
                }
            }

            ScaleGridToScreen();
        }
        
        private void ScaleGridToScreen()
        {
            Camera camera = Camera.main;
            
            float visibleHeight = camera.orthographicSize * 2f;
            float visibleWidth = visibleHeight * camera.aspect;
        
            float targetGridScale = Mathf.Min(
                visibleWidth * 0.85f / _gridConfig.Width,
                visibleHeight * 0.85f / _gridConfig.Height
            );
        
            _gridParent.transform.localScale = Vector3.one * targetGridScale;
        
            float scaledWidth = _gridConfig.Width * targetGridScale;
            float scaledHeight = _gridConfig.Height * targetGridScale;
            _gridParent.transform.position = new Vector3(
                -scaledWidth / 2f + targetGridScale / 2f,
                -scaledHeight / 2f + targetGridScale / 2f,
                0
            );
        }
    }
}