using _Project._Code.Meta.DataConfig;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gird
{
    public class GridSystem : IGridSystem, IInitializable
    {
        private IGridConfig _gridConfig;
        private GridCell.Factory _gridCellFactory;
        private SpawnerPool _spawnerPool;
        
        private GridCell[,] _gridCellArray;
        private GameObject _gridParent;
    
        [Inject]
        public GridSystem(IGridConfig gridConfig, GridCell.Factory gridCellFactory, SpawnerPool spawnerPool)
        {
            _gridConfig =  gridConfig;
            _gridCellFactory = gridCellFactory;
            _spawnerPool = spawnerPool;
        }

        public void Initialize()
        {
            _gridCellArray = new GridCell[_gridConfig.Width, _gridConfig.Height];
            _gridParent = new GameObject("GameGrid");
            for (int x = 0; x < _gridConfig.Width; x++)
            {
                for (int y = 0; y < _gridConfig.Height; y++)
                {
                    GridPosition gridPosition = new GridPosition(x, y);
                    _gridCellArray[x, y] = _gridCellFactory.Create();
                    _gridCellArray[x, y].transform.SetParent(_gridParent.transform);
                    _gridCellArray[x, y].Initialize(gridPosition, _gridConfig.CellSize);
                }
            }

            ScaleGridToScreen();
            AddObjectToGrid(new GridPosition(2, 3));
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

        private void AddObjectToGrid(GridPosition gridPosition)
        {
            GridCell gridCell = TryGetGridCellFromPosition(gridPosition);
            if (gridCell != null)
            {
                Spawner spawner = _spawnerPool.Spawn(gridCell);
                gridCell.AddGridObject(spawner);
            }
        }

        private GridCell TryGetGridCellFromPosition(GridPosition gridPosition)
        {
            if (gridPosition.x >= 0 && gridPosition.x < _gridCellArray.GetLength(0) && gridPosition.y >= 0 && gridPosition.y < _gridCellArray.GetLength(1))
            {
                return _gridCellArray[gridPosition.x, gridPosition.y];
            }
            
            return null;
        }
    }
}