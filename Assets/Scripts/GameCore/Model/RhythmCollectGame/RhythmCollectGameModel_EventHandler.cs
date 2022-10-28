using System;

namespace GameCore
{
    public class RhythmCollectGameModel_EventHandler : ASingleton<RhythmCollectGameModel_EventHandler>
    {
        public event Action<int, int> OnHpChangeTo;
        public event Action OnDead;
        public event Action OnBeat;
        public event Action<RhythmCollectItem> OnClickCollectItem;
        public event Action<int> OnAddPoint;

        public void TriggerHpChangeEvent(int beforeHp, int afterHp)
        {
            OnHpChangeTo?.Invoke(beforeHp, afterHp);
        }

        public void TriggerDeadEvent()
        {
            OnDead?.Invoke();
        }

        public void TriggerBeatEvent()
        {
            OnBeat?.Invoke();
        }

        public void TriggerClickCollectItem(RhythmCollectItem collectItem)
        {
            OnClickCollectItem?.Invoke(collectItem);
        }

        public void TriggerAddPointEvent(int point)
        {
            OnAddPoint?.Invoke(point);
        }
    }
}