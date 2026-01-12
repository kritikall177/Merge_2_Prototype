using System;
using UnityEngine;

namespace _Project._Code.Meta.DataConfig
{
    [Serializable]
    public struct SpawnerParams
    {
        public int SpawnerLvl;
        
        [Range(0, 100)] public float ChanceOnSameLvlSpawn;

        public float ChanceOnNextLvlSpawn => 100f - ChanceOnSameLvlSpawn;
    }
}