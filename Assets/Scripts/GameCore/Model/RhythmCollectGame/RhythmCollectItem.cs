using System;
using System.Linq;

namespace GameCore
{
    public class RhythmCollectItem
    {
        private string collectItemKey;
        private string[] attributeTypes;
        public int basePoint { private set; get; }
        public int baseHpIncrease { private set; get; }
        public bool IsTriggered { private set; get; }
        public bool IsCorrectClick { private set; get; }

        public RhythmCollectItem(string _key, string[] _attributes, int _basePoint, int _baseHpIncrease)
        {
            IsTriggered = false;
            collectItemKey = _key;
            attributeTypes = _attributes;
            basePoint = _basePoint;
            baseHpIncrease = _baseHpIncrease;
        }

        public void TriggerItem(string[] currentHeadings)
        {
            IsTriggered = true;
            IsCorrectClick = IsAttributeMatchHeading(currentHeadings);
            RhythmCollectGameModel_EventHandler.Instance.TriggerClickCollectItem(this);
        }

        private bool IsAttributeMatchHeading(string[] currentHeadings)
        {
            if (currentHeadings == null || currentHeadings.Length <= 0)
                return true;

            if (attributeTypes == null || attributeTypes.Length <= 0)
                return false;

            foreach (string heading in currentHeadings)
            {
                if (attributeTypes.Contains(heading) == false)
                    return false;
            }

            return true;
        }
    }
}