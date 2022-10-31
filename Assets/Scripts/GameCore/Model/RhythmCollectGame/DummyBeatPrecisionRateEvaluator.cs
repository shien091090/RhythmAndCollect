namespace GameCore
{
    public class DummyBeatPrecisionRateEvaluator : IBeatPrecisionRateEvaluator
    {
        public float EvaluateBeatPrecisionRate(float timeOffsetPer)
        {
            return timeOffsetPer;
        }
    }
}