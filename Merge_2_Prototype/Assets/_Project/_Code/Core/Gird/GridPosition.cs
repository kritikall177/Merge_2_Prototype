using System;

namespace _Project._Code.Core.Gird
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int x;
        public int y;

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    
        public static GridPosition operator +(GridPosition a, GridPosition b) => 
            new(a.x + b.x, a.y + b.y);

        public static GridPosition operator -(GridPosition a, GridPosition b) => 
            new(a.x - b.x, a.y - b.y);

        public static bool operator ==(GridPosition a, GridPosition b) => 
            a.x == b.x && a.y == b.y;

        public static bool operator !=(GridPosition a, GridPosition b) => 
            !(a == b);

        public bool Equals(GridPosition other) => 
            x == other.x && y == other.y;

        public override bool Equals(object obj) => 
            obj is GridPosition other && Equals(other);

        public override int GetHashCode() => 
            HashCode.Combine(x, y);

        public override string ToString() => 
            $"x: {x}, y: {y}";
    }
}