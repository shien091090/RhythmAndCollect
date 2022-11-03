using UnityEngine;

namespace GameCore
{
    public class BeatPrecisionRateEvaluator : IBeatPrecisionRateEvaluator
    {
        private AnimationCurve curve;

        public BeatPrecisionRateEvaluator(AnimationCurve _curve)
        {
            curve = _curve;
        }

        public float EvaluateBeatPrecisionRate(float time)
        {
            return curve.Evaluate(time);
        }
    }
}