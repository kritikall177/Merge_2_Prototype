using UnityEngine;

namespace _Project._Code.Meta.DataConfig
{
    public interface IGridConfig
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }
        public Color DefaultColor { get; }
        public Color SelectedColor { get; }
    }
}