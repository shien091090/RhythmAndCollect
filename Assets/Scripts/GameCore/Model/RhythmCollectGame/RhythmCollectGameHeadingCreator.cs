using UnityEngine;

namespace GameCore
{
    public class RhythmCollectGameHeadingCreator : IRhythmCollectGameHeadingCreator
    {
        private AnimationCurve diffficultyToHeadingMixCountCurve;
        private AnimationCurve diffficultyToHeadingMaxCountCurve;

        public RhythmCollectGameHeadingCreator(int diffficulty, AnimationCurve _diffficultyToHeadingMixCountCurve, AnimationCurve _diffficultyToHeadingMaxCountCurve)
        {
            //TODO : 注入一個CollectItemDefineSetting
            diffficultyToHeadingMixCountCurve = _diffficultyToHeadingMixCountCurve;
            diffficultyToHeadingMaxCountCurve = _diffficultyToHeadingMaxCountCurve;
        }

        public string[] CreateHeadings()
        {
            //TODO : 從CollectItemDefineSetting隨機取出一組headings(可能要滿足一些條件, 例如說可生成的物件數量要在多少以上)

            return null;
        }

        public int GetPossibleCreateHeadingsCount()
        {
            //TODO : 從CollectItemDefineSetting取出滿足題目數量n的項目有多少個

            return 10;
        }
    }
}