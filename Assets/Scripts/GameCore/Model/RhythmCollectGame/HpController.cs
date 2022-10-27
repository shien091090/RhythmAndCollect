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

        public event Action<int, int> OnHpChangeTo;
        public event Action OnDead;

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

            OnHpChangeTo?.Invoke(beforeHp, newHp);

            if (currentHp <= hpMin)
                OnDead?.Invoke();
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