namespace GameCore
{
    public class DummyCollectItemSpawnFrequency : ICollectItemSpawnFrequency
    {
        public float GetSpawnFreq(int currentAliveItemCount, int currentBpm)
        {
            float beatFreq = BPMController.GetWaitSecondsToBeatOne(currentBpm);

            if (3 >= currentAliveItemCount) //0~3個 = 1個Beat生成一次
                return beatFreq;
            else if (6 >= currentAliveItemCount) //4~6個 = 2個Beat生成一次
                return beatFreq * 2;
            else if (9 >= currentAliveItemCount) //7~9個 = 3個Beat生成一次
                return beatFreq * 3;
            else //10個以上 = 4個Beat生成一次
                return beatFreq * 4;
        }
    }
}