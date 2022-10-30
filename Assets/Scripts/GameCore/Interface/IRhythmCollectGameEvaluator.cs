public interface IRhythmCollectGameEvaluator
{
    float EvaluateBeatPrecisionRate(float time);
    int EvaluateAddScore(int baseScore, float precisionRate, bool isCorrectClick);
    int EvaluateAddHp(int baseHpIncrease, float precisionRate, bool isCorrectClick);
}
