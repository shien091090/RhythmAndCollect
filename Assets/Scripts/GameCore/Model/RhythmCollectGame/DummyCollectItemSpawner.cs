using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class DummyCollectItemSpawner : IRhythmCollectItemSpawner
    {
        private UpdateTimer spawnTimer;
        private List<RhythmCollectItem> collectItemRecordList;
        private int CurrentAliveItemCount
        {
            get
            {
                if (collectItemRecordList.Count <= 0)
                    return 0;

                return collectItemRecordList.Where(x => x.IsDisappeared == false).Count();
            }
        }

        public DummyCollectItemSpawner()
        {
            collectItemRecordList = new List<RhythmCollectItem>();
        }

        public List<RhythmCollectItem> SpawnNewCollectItemList()
        {
            RhythmCollectItem collectItem = new RhythmCollectItem(string.Empty, new string[] { "A", "B" }, 10, 10);
            return new List<RhythmCollectItem>() { collectItem };
        }
    }
}