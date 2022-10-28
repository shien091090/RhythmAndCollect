public interface IRhythmCollectGameEvaluator
{
    float EvaluateBeatPrecisionRate(float time);
    int EvaluateAddPoint(int basePoint, float precisionRate, bool isCorrectClick);
    int EvaluateAddHp(int baseHpIncrease, float precisionRate, bool isCorrectClick);
}
