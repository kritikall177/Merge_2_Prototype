using System.Collections.Generic;
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
        private ISpawnerConfig _spawnerConfig;
        
        private GridCell[,] _gridCellArray;
        private GameObject _gridParent;
    
        [Inject]
        public GridSystem(IGridConfig gridConfig, GridCell.Factory gridCellFactory, SpawnerPool spawnerPool, ISpawnerConfig  spawnerConfig)
        {
            _gridConfig =  gridConfig;
            _gridCellFactory = gridCellFactory;
            _spawnerPool = spawnerPool;
            _spawnerConfig = spawnerConfig;
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
        }

        public void AddObjectToRandomGridCell(int spawnerLvl, bool withoutCheckingChance = false)
        {
            List<GridPosition> freeGridPositions = new List<GridPosition>(_gridCellArray.Length) ;
            
            for (int x = 0; x < _gridCellArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridCellArray.GetLength(1); y++)
                {
                    GridCell gridCell = _gridCellArray[x, y];
                    if (gridCell && gridCell.GetSpawner() == null)
                    {
                        freeGridPositions.Add(new GridPosition(x, y));
                    }
                }
            }
            
            if (freeGridPositions.Count != 0)
            {
                int randomIndex = Random.Range(0, freeGridPositions.Count);
                if (withoutCheckingChance)
                {
                    AddObjectToGrid(freeGridPositions[randomIndex], spawnerLvl);
                }
                else
                {
                    AddObjectToGrid(freeGridPositions[randomIndex], SpawnerLvlCalculate(spawnerLvl));
                }
            }
        }

        public void TryMergeGridPosition(GridPosition gridPosition1, GridPosition gridPosition2)
        {
            ISpawner spawner = TryGetGridCellFromPosition(gridPosition1).GetSpawner();
            ISpawner spawner2 = TryGetGridCellFromPosition(gridPosition2).GetSpawner();
            if (spawner != null && spawner2 != null)
            {
                if (spawner.SpawnerLvl == spawner2.SpawnerLvl && spawner.SpawnerLvl < _spawnerConfig.MaxLvl)
                {
                    DeleteObjectFromGrid(gridPosition1);
                    DeleteObjectFromGrid(gridPosition2);
                    AddObjectToGrid(gridPosition2, spawner.SpawnerLvl + 1);
                }
            }
        }

        private int SpawnerLvlCalculate(int currentLvl)
        {
            SpawnerParams spawnerParams = _spawnerConfig.GetParams(currentLvl);
            int randomValue = Random.Range(0, 100);
            
            if (randomValue < spawnerParams.ChanceOnPrevLvlSpawn) return Mathf.Max(1, currentLvl - 1);
            if (randomValue < spawnerParams.ChanceOnPrevLvlSpawn + spawnerParams.ChanceOnSameLvlSpawn) return currentLvl;

            return Mathf.Min(_spawnerConfig.MaxLvl, currentLvl + 1);
        }

        private void AddObjectToGrid(GridPosition gridPosition, int spawnerLvl)
        {
            GridCell gridCell = TryGetGridCellFromPosition(gridPosition);
            if (gridCell)
            {
                ISpawner spawner = _spawnerPool.Spawn(gridCell, _spawnerConfig.GetParams(spawnerLvl));
                gridCell.AddSpawner(spawner);
            }
        }

        private void DeleteObjectFromGrid(GridPosition gridPosition)
        {
            GridCell gridCell = TryGetGridCellFromPosition(gridPosition);
            if (gridCell)
            {
                ISpawner gridObject = gridCell.GetSpawner();
                if (gridObject.GameObject)
                {
                    gridCell.DeleteSpawner();
                    _spawnerPool.Despawn(gridObject);
                }
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

        private void ScaleGridToScreen()
        {
            Camera camera = Camera.main;
            
            float visibleHeight = camera.orthographicSize * 2f;
            float visibleWidth = visibleHeight * camera.aspect;
        
            float offsetCoefficient = 0.75f;
            
            float targetGridScale = Mathf.Min(
                visibleWidth * offsetCoefficient / _gridConfig.Width,
                visibleHeight * offsetCoefficient / _gridConfig.Height
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