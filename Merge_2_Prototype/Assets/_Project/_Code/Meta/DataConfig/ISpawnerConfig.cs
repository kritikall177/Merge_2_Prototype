namespace _Project._Code.Meta.DataConfig
{
    public interface ISpawnerConfig
    {
        public SpawnerParams GetParams(int level);
        public int MaxLvl { get; }
    }
}