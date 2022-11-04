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
        private UpdateTimer lifeTimer;

        public RhythmCollectItem(string _key, string[] _attributes, int _baseScore, int _baseHpIncrease, float _lifeTime)
        {
            Init(new RhythmCollectItemAttribute(_key, _attributes, _baseScore, _baseHpIncrease, _lifeTime));
        }

        public RhythmCollectItem(RhythmCollectItemAttribute _attributeInfo)
        {
            Init(_attributeInfo);
        }

        private void Init(RhythmCollectItemAttribute _attributeInfo)
        {
            attributeInfo = _attributeInfo;
            lifeTimer = new UpdateTimer(attributeInfo.lifeTime);
            IsTriggered = false;

            lifeTimer.OnTriggerTimer += Disappear;
        }

        public void Update(float deltaTime)
        {
            if (lifeTimer != null)
                lifeTimer.Update(deltaTime);
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
            lifeTimer.OnTriggerTimer -= Disappear;

            IsDisappeared = true;
            RhythmCollectGameModel_EventHandler.Instance.TriggerCollectItemDisappearEvent(this);
        }

        public bool IsAttributeMatchHeading(string[] currentHeadings)
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