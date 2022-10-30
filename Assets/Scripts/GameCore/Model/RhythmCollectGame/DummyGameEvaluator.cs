public class DummyGameEvaluator : IRhythmCollectGameEvaluator
{
    public int EvaluateAddHp(int baseHpIncrease, float precisionRate, bool isCorrectClick)
    {
        if (isCorrectClick)
            return (int)( baseHpIncrease * precisionRate );
        else
            return -5;
    }

    public int EvaluateAddScore(int baseScore, float precisionRate, bool isCorrectClick)
    {
        if (isCorrectClick)
            return (int)( baseScore * precisionRate );
        else
            return 0;
    }

    public float EvaluateBeatPrecisionRate(float timeOffsetPer)
    {
        return timeOffsetPer;
    }
}
