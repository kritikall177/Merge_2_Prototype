using System.Collections;
using _Project._Code.Core.Gird;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project._Code.Meta.UI
{
    public class SpawnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _cooldownTime = 3f;
        
        private IGridSystem _gridSystem;
        private Coroutine _cooldownCoroutine;
        
        [Inject]
        public void Construct(IGridSystem gridSystem)
        {
            _gridSystem = gridSystem;
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (_cooldownCoroutine != null) return;
            
            _cooldownCoroutine = StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            _button.interactable = false;
            float timer = _cooldownTime;
            
            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            
            Spawn();
            _button.interactable = true;
            _cooldownCoroutine = null;
        }

        private void Spawn()
        {
            _gridSystem.AddObjectToRandomGridCell(1);
        }

        private void OnDestroy()
        {
            if (_button) _button.onClick.RemoveListener(OnButtonClick);
            
            if (_cooldownCoroutine != null) StopCoroutine(_cooldownCoroutine);
        }
    }
}