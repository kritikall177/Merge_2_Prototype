using _Project._Code.Meta.DataConfig;
using UnityEngine;

namespace _Project._Code.Core.SpawnerObject
{
    public interface ISpawner
    {
        public int SpawnerLvl { get; }
        public GameObject GameObject { get; }

        public void SetParams(SpawnerParams spawnerParams);
        public void Activate();
    }
}