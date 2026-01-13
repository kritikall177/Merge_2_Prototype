namespace _Project._Code.Core.Gird
{
    public interface IGridSystem
    {
        public void AddObjectToRandomGridCell(int spawnerLvl, bool withoutCheckingChance = false);
        public void TryMergeGridPosition(GridPosition position1, GridPosition position2);
    }
}