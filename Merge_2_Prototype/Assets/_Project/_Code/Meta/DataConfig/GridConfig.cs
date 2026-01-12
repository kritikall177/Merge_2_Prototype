using UnityEngine;

namespace _Project._Code.Meta.DataConfig
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Data/Grid Config")]
    public class GridConfig : ScriptableObject, IGridConfig
    {
        [field: SerializeField, Min(0)] public int Width { get; set; }
        [field: SerializeField, Min(0)] public int Height { get; set; }
        [field: SerializeField, Range(0.3f, 0.4f)] public float CellSize { get; set; }
    }
}