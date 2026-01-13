using _Project._Code.Core.Gird;
using _Project._Code.Meta.DataConfig;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.SpawnerObject
{
    public class Spawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private TMP_Text _lvlText;
        
        public int SpawnerLvl { get; private set; }
        public GameObject GameObject => gameObject;

        private IGridSystem _gridSystem;

        [Inject]
        public void Construct(IGridSystem gridSystem)
        {
            _gridSystem = gridSystem;
        }

        public void SetParams(SpawnerParams  spawnerParams)
        {
            SpawnerLvl =  spawnerParams.SpawnerLvl;
            _lvlText.SetText($"{SpawnerLvl}");
        }

        public void Activate() => _gridSystem.AddObjectToRandomGridCell(SpawnerLvl);
    }
}