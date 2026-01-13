using System.Collections;
using _Project._Code.Core.Gird;
using _Project._Code.Meta.DataConfig;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core
{
    public class Spawner : MonoBehaviour, ISpawner
    {
        public int SpawnerLvl { get; private set; }
        public GameObject GameObject => gameObject;
        
        [SerializeField] private TMP_Text _lvlText;
        
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

        public void Activate()
        {
            Debug.Log("Activate Spawner");
            _gridSystem.AddObjectToRandomGridCell(SpawnerLvl);
        }
    }

    public interface ISpawner
    {
        public int SpawnerLvl { get; }
        
        public GameObject GameObject { get; }

        public void SetParams(SpawnerParams spawnerParams);
        public void Activate();
    }
}