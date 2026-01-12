using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Project._Code.Meta.DataConfig
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Data/Spawner Config")]
    public class SpawnerConfig : ScriptableObject, ISpawnerConfig
    {
        [SerializeField] private List<SpawnerParams> _spawnerParams = new();

        public IReadOnlyList<SpawnerParams> SpawnerParams => _spawnerParams;

        public bool TryGetParams(int level, out SpawnerParams param)
        {
            param = default;

            if (level < 1 || level > _spawnerParams.Count)
                return false;

            param = _spawnerParams[level - 1];
            return true;
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            LevelUpdater();
        }

        private void LevelUpdater()
        {
            for (var i = 0; i < _spawnerParams.Count; i++)
            {
                var param = _spawnerParams[i];
                param.SpawnerLvl = i + 1;
                _spawnerParams[i] = param;
            }
        }
#endif
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SpawnerParams))]
    public class SpawnerParamsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var levelProp = property.FindPropertyRelative("SpawnerLvl");
            var sameChanceProp = property.FindPropertyRelative("ChanceOnSameLvlSpawn");

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, $"Уровень {levelProp.intValue}", EditorStyles.boldLabel);
            position.y += EditorGUIUtility.singleLineHeight + 4;

            var sameChance = sameChanceProp.floatValue;
            var nextChance = 100f - sameChance;

            EditorGUI.Slider(position, sameChanceProp, 0, 100,
                new GUIContent($"Шанс этого уровня ({sameChance:F1}%)",
                    "Вероятность появления предмета текущего уровня"));
            position.y += EditorGUIUtility.singleLineHeight + 2;

            EditorGUI.LabelField(position, $"Шанс след. уровня: {nextChance:F1}%", EditorStyles.miniLabel);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight + 2) * 3 + 2;
        }
    }
#endif
    public interface ISpawnerConfig
    {
        bool TryGetParams(int level, out SpawnerParams param);
    }
}