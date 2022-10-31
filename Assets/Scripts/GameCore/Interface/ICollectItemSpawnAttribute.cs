using System.Collections.Generic;

namespace GameCore
{
    public interface ICollectItemSpawnAttribute
    {
        RhythmCollectItemAttribute GetCollectItemAttribute(List<RhythmCollectItem> currentAliveItemList, string[] currentHeadings);
    }
}