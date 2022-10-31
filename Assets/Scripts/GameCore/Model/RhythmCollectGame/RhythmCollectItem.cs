using System;
using System.Linq;

namespace GameCore
{
    public class RhythmCollectItem
    {
        private RhythmCollectItemAttribute attributeInfo;
        public int GetBaseScore { get { return attributeInfo.baseScore; } }
        public int GetBaseHpIncrease { get { return attributeInfo.baseHpIncrease; } }
        public bool IsTriggered { private set; get; }
        public bool IsCorrectClick { private set; get; }
        public bool IsDisappeared { private set; get; }

        public RhythmCollectItem(string _key, string[] _attributes, int _baseScore, int _baseHpIncrease)
        {
            attributeInfo = new RhythmCollectItemAttribute(_key, _attributes, _baseScore, _baseHpIncrease);
            IsTriggered = false;
        }

        public RhythmCollectItem(RhythmCollectItemAttribute _attributeInfo)
        {
            attributeInfo = _attributeInfo;
            IsTriggered = false;
        }

        public void TriggerItem(string[] currentHeadings)
        {
            if (IsDisappeared)
                return;

            IsTriggered = true;
            IsCorrectClick = IsAttributeMatchHeading(currentHeadings);
            RhythmCollectGameModel_EventHandler.Instance.TriggerClickCollectItem(this);
            Disappear();
        }

        public void Disappear()
        {
            IsDisappeared = true;
            RhythmCollectGameModel_EventHandler.Instance.TriggerCollectItemDisappearEvent(this);
        }

        private bool IsAttributeMatchHeading(string[] currentHeadings)
        {
            if (currentHeadings == null || currentHeadings.Length <= 0)
                return true;

            if (attributeInfo.attributeTypes == null || attributeInfo.attributeTypes.Length <= 0)
                return false;

            foreach (string heading in currentHeadings)
            {
                if (attributeInfo.attributeTypes.Contains(heading) == false)
                    return false;
            }

            return true;
        }
    }
}