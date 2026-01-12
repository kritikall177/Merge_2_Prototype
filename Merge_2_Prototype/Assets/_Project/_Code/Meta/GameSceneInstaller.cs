using _Project._Code.Core;
using _Project._Code.Core.Gird;
using _Project._Code.Meta.DataConfig;
using _Project._Code.Meta.Input;
using UnityEngine;
using Zenject;

namespace _Project._Code.Meta
{
    public class GameSceneInstaller :  MonoInstaller
    {
        [SerializeField] private GridCell _gridCellPrefab;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private GridConfig _gridConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputSystemService>().AsSingle().NonLazy();
            
            Container.Bind<IGridConfig>().FromInstance(_gridConfig).AsSingle();
            
            Container.BindInterfacesTo<GridSystem>().AsSingle();

            Container.BindInterfacesTo<TriggerRayEmitter>().FromNew().AsSingle().NonLazy();
            
            Container.BindFactory<GridCell, GridCell.Factory>().FromComponentInNewPrefab(_gridCellPrefab);
            
            Container.BindMemoryPool<Spawner, SpawnerPool>().FromComponentInNewPrefab(_spawner);
        }
    }
}