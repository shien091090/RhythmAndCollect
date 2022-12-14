using System;
using System.Reflection;

namespace GameCore
{
    public class RhythmCollectGameModel_EventHandler : ASingleton<RhythmCollectGameModel_EventHandler>
    {
        public event Action<int> OnInitBPM;
        public event Action<string[]> OnInitHeadings;
        public event Action<int, int> OnHpChangeTo;
        public event Action OnDead;
        public event Action OnBeat;
        public event Action OnHalfBeat;
        public event Action<RhythmCollectItem> OnClickCollectItem;
        public event Action<RhythmCollectItem> OnCollectItemDisappeared;
        public event Action<RhythmCollectItem> OnSpawnCollectItem;
        public event Action<int> OnAddScore;
        public event Action<string[]> OnHeadingsUpdated;

        public void ClearAllEvent()
        {
            RebuildInstance();
        }

        public void TriggerInitBPMEvent(int bpm)
        {
            OnInitBPM?.Invoke(bpm);
        }

        public void TriggerInitHeadingsEvent(string[] headings)
        {
            OnInitHeadings?.Invoke(headings);
        }

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

        public void TriggerHalfBeatEvent()
        {
            OnHalfBeat?.Invoke();
        }

        public void TriggerClickCollectItem(RhythmCollectItem collectItem)
        {
            OnClickCollectItem?.Invoke(collectItem);
        }

        public void TriggerCollectItemDisappearEvent(RhythmCollectItem collectItem)
        {
            OnCollectItemDisappeared?.Invoke(collectItem);
        }

        public void TriggerSpawnCollectItemEvent(RhythmCollectItem collectItem)
        {
            OnSpawnCollectItem?.Invoke(collectItem);
        }

        public void TriggerAddScoreEvent(int score)
        {
            OnAddScore?.Invoke(score);
        }

        public void TriggerUpdateHeadingsEvent(string[] newHeadings)
        {
            OnHeadingsUpdated?.Invoke(newHeadings);
        }
    }
}