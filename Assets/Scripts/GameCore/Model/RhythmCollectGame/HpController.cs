using System;

namespace GameCore
{
    public class HpController
    {
        private int hpMax;
        private int hpMin;
        private int currentHp;

        public bool IsDead
        {
            get
            {
                return currentHp <= hpMin;
            }
        }

        public HpController(int _hpMax, int _hpMin = 0)
        {
            hpMax = _hpMax;
            hpMin = _hpMin;
            currentHp = _hpMax;
        }

        public void SetHp(int newHp)
        {
            int beforeHp = currentHp;
            currentHp = newHp;

            RhythmCollectGameModel_EventHandler.Instance.TriggerHpChangeEvent(beforeHp, newHp);

            if (currentHp <= hpMin)
                RhythmCollectGameModel_EventHandler.Instance.TriggerDeadEvent();
        }

        public void AddHp(int increase)
        {
            int newHp = currentHp + increase;

            if (newHp > hpMax)
                newHp = hpMax;
            else if (newHp < hpMin)
                newHp = hpMin;

            SetHp(newHp);
        }
    }
}