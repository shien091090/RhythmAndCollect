using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class CollectItemAttributeSpawner : ICollectItemSpawnAttribute
    {
        private AnimationCurve currentAliveAndMatchCountToSpawnMatchItemRateCurve;

        public CollectItemAttributeSpawner(AnimationCurve _currentAliveAndMatchCountToSpawnMatchItemRateCurve)
        {
            currentAliveAndMatchCountToSpawnMatchItemRateCurve = _currentAliveAndMatchCountToSpawnMatchItemRateCurve;
        }

        public RhythmCollectItemAttribute GetCollectItemAttribute(List<RhythmCollectItem> currentAliveItemList, string[] currentHeadings)
        {
            //TODO : 從CollectItemDefineSetting拿Attribute
            RhythmCollectItemAttribute attribute = new RhythmCollectItemAttribute(string.Empty, null, 10, 10, 10);
            return attribute;
        }

        private int GetAliveItemAndMatchHeadingCount(List<RhythmCollectItem> currentAliveItemList, string[] currentHeadings)
        {
            int matchCount = currentAliveItemList.Where(x => x.IsAttributeMatchHeading(currentHeadings)).Count();
            return matchCount;
        }
    }
}