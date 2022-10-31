namespace GameCore
{
    public interface ICollectItemSpawnFrequency
    {
        float GetSpawnFreq(int currentAliveItemCount, int currentBpm);
    }
}