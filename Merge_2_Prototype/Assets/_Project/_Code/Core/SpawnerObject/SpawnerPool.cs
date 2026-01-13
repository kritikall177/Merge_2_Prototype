using _Project._Code.Core.Gird;
using _Project._Code.Meta.DataConfig;
using Zenject;

namespace _Project._Code.Core.SpawnerObject
{
    public class SpawnerPool : MemoryPool<GridCell, SpawnerParams, ISpawner>
    {
        protected override void OnSpawned(ISpawner spawner)
        {
            spawner.GameObject.SetActive(true);
        }

        protected override void Reinitialize(GridCell gridCell, SpawnerParams spawnerParams, ISpawner spawner)
        {
            spawner.SetParams(spawnerParams);
            spawner.GameObject.transform.SetParent(gridCell.transform, false);
        }

        protected override void OnDespawned(ISpawner spawner)
        {
            spawner.GameObject.SetActive(false);
        }
    }
}