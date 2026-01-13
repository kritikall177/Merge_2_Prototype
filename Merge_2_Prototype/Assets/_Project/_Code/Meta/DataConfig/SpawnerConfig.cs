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
        [SerializeField] private List<SpawnerParams> _spawnerParamsList = new();

        public IReadOnlyList<SpawnerParams> SpawnerParamsList => _spawnerParamsList;
        public int MaxLvl => _spawnerParamsList.Count;

        public SpawnerParams GetParams(int level)
        {
            if (level < 1) return _spawnerParamsList[0];
            if (level > _spawnerParamsList.Count) return _spawnerParamsList[^1];
            return _spawnerParamsList[level - 1];
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            LevelUpdater();
        }

        private void LevelUpdater()
        {
            for (var i = 0; i < _spawnerParamsList.Count; i++)
            {
                var param = _spawnerParamsList[i];
                param.SpawnerLvl = i + 1;
                _spawnerParamsList[i] = param;
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
            var prevChanceProp = property.FindPropertyRelative("ChanceOnPrevLvlSpawn");
            var sameChanceProp = property.FindPropertyRelative("ChanceOnSameLvlSpawn");

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, $"Уровень {levelProp.intValue}", EditorStyles.boldLabel);
            position.y += EditorGUIUtility.singleLineHeight + 4;

            var prevChance = prevChanceProp.intValue;
            var sameChance = sameChanceProp.intValue;
            var nextChance = 100f - prevChance - sameChance;

            var maxForPrev = 100 - sameChance;
            var maxForSame = 100 - prevChance;

            EditorGUI.IntSlider(position, prevChanceProp, 0, maxForPrev,
                new GUIContent($"Шанс уровня ниже ({prevChance}%)"));
            position.y += EditorGUIUtility.singleLineHeight + 2;

            prevChance = prevChanceProp.intValue;
            maxForSame = 100 - prevChance;

            EditorGUI.IntSlider(position, sameChanceProp, 0, maxForSame,
                new GUIContent($"Шанс этого уровня ({sameChance}%)"));
            position.y += EditorGUIUtility.singleLineHeight + 2;

            sameChance = sameChanceProp.intValue;
            nextChance = 100f - prevChance - sameChance;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.FloatField(position, "Шанс уровня выше", nextChance);
            EditorGUI.EndDisabledGroup();
            position.y += EditorGUIUtility.singleLineHeight + 2;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight + 2) * 5 + 2;
        }
    }
#endif
}