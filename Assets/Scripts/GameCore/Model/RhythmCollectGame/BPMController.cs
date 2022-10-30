using System;
using UnityEngine;

namespace GameCore
{
    public class BPMController
    {
        private int bpm;
        IRhythmCollectGameEvaluator evaluator;
        public bool isAlreadyBeatFirstTime { private set; get; }
        private bool isHalfBeat;
        private float timer;
        private float totalTime;

        public float GetWaitSecondsToBeatOne
        {
            get
            {
                return 60 / (float)bpm;
            }
        }

        public BPMController(int _bpm, IRhythmCollectGameEvaluator _evaluator)
        {
            timer = 0;
            evaluator = _evaluator;
            isAlreadyBeatFirstTime = false;
            SetBPM(_bpm);
        }

        public void SetBPM(int _bpm)
        {
            bpm = _bpm;
        }

        public void Update(float deltaTime)
        {
            timer += deltaTime;
            totalTime += deltaTime;

            if(timer >= GetWaitSecondsToBeatOne / 2 &&
                isHalfBeat == false)
            {
                RhythmCollectGameModel_EventHandler.Instance.TriggerHalfBeatEvent();
                isHalfBeat = true;
            }

            if (timer >= GetWaitSecondsToBeatOne)
            {
                RhythmCollectGameModel_EventHandler.Instance.TriggerBeatEvent();
                RhythmCollectGameModel_EventHandler.Instance.TriggerHalfBeatEvent();
                isAlreadyBeatFirstTime = true;
                isHalfBeat = false;
                timer = 0;
            }

        }

        public float GetBeatPrecisionRate()
        {
            if (isAlreadyBeatFirstTime == false)
                return 0;

            if (evaluator == null)
                return 0;

            float timeOffsetPer = Math.Abs(timer - ( GetWaitSecondsToBeatOne / 2 ) / ( GetWaitSecondsToBeatOne / 2 ));
            float rate = evaluator.EvaluateBeatPrecisionRate(timeOffsetPer);
            return rate;
        }
    }
}