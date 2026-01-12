using _Project._Code.Core.Gird;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core
{
    public class SpawnerPool : MemoryPool<GridCell, Spawner>
    {
        protected override void OnSpawned(Spawner spawner)
        {
            spawner.gameObject.SetActive(true);
        }

        protected override void Reinitialize(GridCell gridCell, Spawner spawner)
        {
            spawner.transform.SetParent(gridCell.transform, false);
        }

        protected override void OnDestroyed(Spawner spawner)
        {
            // Called immediately after the item is removed from the pool without also being spawned
            // This occurs when the pool is shrunk either by using WithMaxSize or by explicitly shrinking the pool by calling the `ShrinkBy` / `Resize methods
        }

        protected override void OnDespawned(Spawner spawner)
        {
            spawner.gameObject.SetActive(false);
        }
    }
}