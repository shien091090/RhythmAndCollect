using System;

namespace GameCore
{
    public class BPMController
    {
        private int bpm;

        public float GetWaitSecondsToBeatOne
        {
            get
            {
                return 60 / (float)bpm;
            }
        }

        private float timer;

        public event Action OnBeat;

        public BPMController(int _bpm)
        {
            timer = 0;
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
                timer = 0;
            }

        }

    }
}