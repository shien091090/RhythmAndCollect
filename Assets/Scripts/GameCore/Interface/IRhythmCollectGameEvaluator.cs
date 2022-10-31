namespace GameCore
{
    public interface IRhythmCollectGameEvaluator
    {
        int EvaluateAddScore(int baseScore, float precisionRate, bool isCorrectClick);
        int EvaluateAddHp(int baseHpIncrease, float precisionRate, bool isCorrectClick);
    }
}