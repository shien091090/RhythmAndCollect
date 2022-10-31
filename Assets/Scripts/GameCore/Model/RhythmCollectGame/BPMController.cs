using System;
using UnityEngine;

namespace GameCore
{
    public class BPMController
    {
        private int bpm;
        IRhythmCollectGameEvaluator evaluator;
        public bool isAlreadyBeatFirstTime { private set; get; }
        private UpdateTimer mainTimer;
        private UpdateTimer halfTimer;

        public float GetWaitSecondsToBeatOne
        {
            get
            {
                return 60 / (float)bpm;
            }
        }

        public BPMController(int _bpm, IRhythmCollectGameEvaluator _evaluator)
        {
            evaluator = _evaluator;
            isAlreadyBeatFirstTime = false;
            SetBPM(_bpm);
            mainTimer = new UpdateTimer(GetWaitSecondsToBeatOne);
            halfTimer = new UpdateTimer(GetWaitSecondsToBeatOne / 2);

            mainTimer.OnTriggerTimer += OnTriggerMainTimer;
            halfTimer.OnTriggerTimer += OnTriggerHalfTimer;
        }

        public void SetBPM(int _bpm)
        {
            bpm = _bpm;
        }

        public void Update(float deltaTime)
        {
            mainTimer.Update(deltaTime);
            halfTimer.Update(deltaTime);
        }

        private void OnTriggerMainTimer()
        {
            isAlreadyBeatFirstTime = true;
            RhythmCollectGameModel_EventHandler.Instance.TriggerBeatEvent();
        }

        private void OnTriggerHalfTimer()
        {
            RhythmCollectGameModel_EventHandler.Instance.TriggerHalfBeatEvent();
        }

        public float GetBeatPrecisionRate()
        {
            if (isAlreadyBeatFirstTime == false)
                return 0;

            if (evaluator == null)
                return 0;

            float timeOffsetPer = Math.Abs(mainTimer.timer - ( GetWaitSecondsToBeatOne / 2 ) / ( GetWaitSecondsToBeatOne / 2 ));
            float rate = evaluator.EvaluateBeatPrecisionRate(timeOffsetPer);
            return rate;
        }
    }
}