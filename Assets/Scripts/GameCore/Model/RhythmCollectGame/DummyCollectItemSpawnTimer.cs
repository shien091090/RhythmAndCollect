namespace GameCore
{
    public class DummyCollectItemSpawnFrequency : ICollectItemSpawnFrequency
    {
        public float GetSpawnFreq(int currentAliveItemCount, int currentBpm)
        {
            float beatFreq = BPMController.GetWaitSecondsToBeatOne(currentBpm);
            float result = currentAliveItemCount / beatFreq;

            if (result <= 0)
                return 0.5f;
            else
                return currentAliveItemCount / beatFreq;
        }
    }
}