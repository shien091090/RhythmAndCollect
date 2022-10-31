using System;

namespace GameCore
{
    public class UpdateTimer
    {
        public float timer { private set; get; }
        public float totalTime { private set; get; }
        private float resetTimeThreshold;

        public Action OnTriggerTimer;

        public UpdateTimer(float _resetTimeThreshold)
        {
            SetResetTimeThreshold(_resetTimeThreshold);
        }

        public void SetResetTimeThreshold(float _resetTimeThreshold)
        {
            resetTimeThreshold = _resetTimeThreshold;
        }

        public void Update(float deltaTime)
        {
            timer += deltaTime;
            totalTime += deltaTime;

            if(timer >= resetTimeThreshold)
            {
                timer = 0;
                OnTriggerTimer?.Invoke();
            }
        }
    }
}