using System;

namespace _Project._Code.Core.Gird
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int x;
        public int z;

        public GridPosition(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
    
        public static GridPosition operator +(GridPosition a, GridPosition b) => 
            new(a.x + b.x, a.z + b.z);

        public static GridPosition operator -(GridPosition a, GridPosition b) => 
            new(a.x - b.x, a.z - b.z);

        public static bool operator ==(GridPosition a, GridPosition b) => 
            a.x == b.x && a.z == b.z;

        public static bool operator !=(GridPosition a, GridPosition b) => 
            !(a == b);

        public bool Equals(GridPosition other) => 
            x == other.x && z == other.z;

        public override bool Equals(object obj) => 
            obj is GridPosition other && Equals(other);

        public override int GetHashCode() => 
            HashCode.Combine(x, z);

        public override string ToString() => 
            $"x: {x}, y: {z}";
    }
}