using UnityEngine;

namespace GameCore
{
    public class RhythmCollectGameEvaluator : IRhythmCollectGameEvaluator
    {
        private AnimationCurve correctClickEvaluateAddHpCurve;
        private AnimationCurve difficultyToDamageHpCurve;
        private AnimationCurve correctClickEvaluateAddScoreCurve;
        private AnimationCurve incorrectClickEvaluateAddScoreCurve;
        private int difficulty;

        public RhythmCollectGameEvaluator(AnimationCurve _correctClickEvaluateAddHpCurve, AnimationCurve _difficultyToDamageHpCurve, AnimationCurve _correctClickEvaluateAddScoreCurve, AnimationCurve _incorrectClickEvaluateAddScoreCurve, int _difficulty)
        {
            correctClickEvaluateAddHpCurve = _correctClickEvaluateAddHpCurve;
            difficultyToDamageHpCurve = _difficultyToDamageHpCurve;
            correctClickEvaluateAddScoreCurve = _correctClickEvaluateAddScoreCurve;
            incorrectClickEvaluateAddScoreCurve = _incorrectClickEvaluateAddScoreCurve;
            difficulty = _difficulty;
        }

        public int EvaluateAddHp(int baseHpIncrease, float precisionRate, bool isCorrectClick)
        {
            if (isCorrectClick)
            {
                int addHp = (int)( correctClickEvaluateAddHpCurve.Evaluate(precisionRate) * baseHpIncrease );
                return addHp;
            }
            else
            {
                int damageHp = (int)difficultyToDamageHpCurve.Evaluate(difficulty);
                return damageHp;
            }

        }

        public int EvaluateAddScore(int baseScore, float precisionRate, bool isCorrectClick)
        {
            int addScore = 0;

            if (isCorrectClick)
                addScore = (int)( correctClickEvaluateAddScoreCurve.Evaluate(precisionRate) * baseScore );
            else
                addScore = (int)( incorrectClickEvaluateAddScoreCurve.Evaluate(precisionRate) * baseScore );

            return addScore;
        }
    }
}
