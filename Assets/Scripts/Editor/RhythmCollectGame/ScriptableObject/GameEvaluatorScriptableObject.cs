using UnityEngine;
namespace GameCore
{
    public class GameEvaluatorScriptableObject : ScriptableObject, IGameSettingGetter<IRhythmCollectGameEvaluator>
    {
        [SerializeField] private AnimationCurve correctClickEvaluateAddHpCurve;
        [SerializeField] private AnimationCurve difficultyToDamageHpCurve;
        [SerializeField] private AnimationCurve correctClickEvaluateAddScoreCurve;
        [SerializeField] private AnimationCurve incorrectClickEvaluateAddScoreCurve;

        public IRhythmCollectGameEvaluator GetGameSetting(int difficulty = 0)
        {
            RhythmCollectGameEvaluator evaluator = new RhythmCollectGameEvaluator(
                correctClickEvaluateAddHpCurve,
                difficultyToDamageHpCurve,
                correctClickEvaluateAddScoreCurve,
                incorrectClickEvaluateAddScoreCurve,
                difficulty);

            return evaluator;
        }
    }
}