using UnityEngine;

namespace _Project._Code.Meta.DataConfig
{
    [System.Serializable]
    public struct SpawnerParams
    {
        public int SpawnerLvl;
        
        [Range(0, 100)] public int ChanceOnPrevLvlSpawn;
        
        [Range(0, 100)] public int ChanceOnSameLvlSpawn;
        
        public float ChanceOnNextLvlSpawn => 100f - ChanceOnPrevLvlSpawn - ChanceOnSameLvlSpawn;
        
    }
}