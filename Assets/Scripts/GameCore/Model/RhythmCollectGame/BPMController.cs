using System;
using UnityEngine;

namespace GameCore
{
    public class BPMController
    {
        private int bpm;
        private Func<float, float> evaluateFunc;
        private bool isAlreadyBeatFirstTime;

        public float GetWaitSecondsToBeatOne
        {
            get
            {
                return 60 / (float)bpm;
            }
        }

        private float timer;

        public event Action OnBeat;

        public BPMController(int _bpm, Func<float, float> _evaluateFunc)
        {
            timer = 0;
            evaluateFunc = _evaluateFunc;
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

            if (timer >= GetWaitSecondsToBeatOne)
            {
                OnBeat?.Invoke();
                isAlreadyBeatFirstTime = true;
                timer = 0;
            }

        }

        public float GetBeatPrecisionRate()
        {
            if (isAlreadyBeatFirstTime == false)
                return 0;

            if (evaluateFunc == null)
                return 0;

            float timeOffsetPer = Math.Abs(timer - ( GetWaitSecondsToBeatOne / 2 ) / ( GetWaitSecondsToBeatOne / 2 ));
            float rate = evaluateFunc.Invoke(timeOffsetPer);
            return rate;
        }
    }
}