using UnityEngine;

namespace GameCore
{
    public class CollectItemSpawnSettingScriptableObject : ScriptableObject, IGameSettingGetter<RhythmCollectItemSpawner>
    {
        [SerializeField] private AnimationCurve currentItemCountToSpawnFreqCurve;
        [SerializeField] private AnimationCurve currentAliveAndMatchCountToSpawnMatchItemRateCurve;

        public RhythmCollectItemSpawner GetGameSetting(int difficulty = 0)
        {
            CollectItemSpawnFreqController spawnFreqController = new CollectItemSpawnFreqController(currentItemCountToSpawnFreqCurve);
            CollectItemAttributeSpawner attributeSpawner = new CollectItemAttributeSpawner(currentAliveAndMatchCountToSpawnMatchItemRateCurve);
            RhythmCollectItemSpawner collectItemSpawner = new RhythmCollectItemSpawner(spawnFreqController, attributeSpawner);

            return collectItemSpawner;
        }
    }
}