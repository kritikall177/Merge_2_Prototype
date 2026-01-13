using _Project._Code.Core.Gird;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project._Code.Meta.UI
{
    public class SpawnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private IGridSystem _gridSystem;
        
        [Inject]
        public void Construct(IGridSystem gridSystem)
        {
            _gridSystem = gridSystem;
            _button.onClick.AddListener(Spawn);
        }

        private void Spawn()
        {
            _gridSystem.AddObjectToRandomGridCell(1, 100);
        }
    }
}