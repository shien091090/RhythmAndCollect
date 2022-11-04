using UnityEngine;

namespace GameCore
{
    public class CollectItemSpawnFreqController : ICollectItemSpawnFrequency
    {
        private AnimationCurve currentItemCountToSpawnFreqCurve;

        public CollectItemSpawnFreqController(AnimationCurve _currentItemCountToSpawnFreqCurve)
        {
            currentItemCountToSpawnFreqCurve = _currentItemCountToSpawnFreqCurve;
        }

        public float GetSpawnFreq(int currentAliveItemCount, int currentBpm)
        {
            float beatFreq = BPMController.GetWaitSecondsToBeatOne(currentBpm);
            float spawnFreq = beatFreq * currentItemCountToSpawnFreqCurve.Evaluate(currentAliveItemCount);

            return spawnFreq;
        }
    }
}