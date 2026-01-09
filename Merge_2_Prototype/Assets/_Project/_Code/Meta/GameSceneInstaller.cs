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
        [SerializeField] private GridConfig _gridConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputSystemService>().AsSingle().NonLazy();
            
            Container.Bind<IGridConfig>().FromInstance(_gridConfig).AsSingle();
            
            Container.BindFactory<GridCell, GridCell.Factory>().FromComponentInNewPrefab(_gridCellPrefab);
            
            Container.BindInterfacesTo<GridSystem>().AsSingle();
        }
    }
}