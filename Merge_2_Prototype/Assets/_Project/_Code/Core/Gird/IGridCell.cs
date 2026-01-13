namespace _Project._Code.Core.Gird
{
    public interface IGridCell
    {
        public GridPosition GridPosition { get; }
        public void TriggerGridCell();
        public void SelectGridCell();
        public void UnselectGridCell();
    }
}