using System.Collections.Generic;

namespace GameCore
{
    public interface IRhythmCollectItemSpawner
    {
        List<RhythmCollectItem> SpawnNewCollectItemList();
    }
}