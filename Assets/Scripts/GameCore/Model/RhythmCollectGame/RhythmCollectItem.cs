using System;
using System.Linq;

namespace GameCore
{
    public class RhythmCollectItem
    {
        private string collectItemKey;
        private string[] attributeTypes;
        private int basePoint;
        private int baseHpIncrease;
        public bool isTriggered { private set; get; }

        public event Action<bool> OnTriggerMatchHeadingItem;

        public RhythmCollectItem(string _key, string[] _attributes, int _basePoint, int _baseHpIncrease)
        {
            isTriggered = false;
            collectItemKey = _key;
            attributeTypes = _attributes;
            basePoint = _basePoint;
            baseHpIncrease = _baseHpIncrease;
        }

        public void TriggerItem(string[] currentHeadings)
        {
            isTriggered = true;
            bool isMatchHeading = IsAttributeMatchHeading(currentHeadings);
            OnTriggerMatchHeadingItem?.Invoke(isMatchHeading);
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