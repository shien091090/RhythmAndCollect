using System.Collections.Generic;

namespace GameCore
{
    public class DummyCollectItemSpawnAttribute : ICollectItemSpawnAttribute
    {
        public RhythmCollectItemAttribute GetCollectItemAttribute(List<RhythmCollectItem> currentAliveItemList, string[] currentHeadings)
        {
            if (currentAliveItemList == null || currentAliveItemList.Count <= 5) //場上的物件在5個以下時, 產生符合題目的Attribute
                return new RhythmCollectItemAttribute(string.Empty, currentHeadings, 10, 10, 10);
            else //場上的物件在5個以上時, 產生不符合題目的Attribute
                return new RhythmCollectItemAttribute(string.Empty, null, 10, 10, 10);

        }
    }
}