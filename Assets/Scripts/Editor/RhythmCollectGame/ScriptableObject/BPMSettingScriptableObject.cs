using UnityEngine;

namespace GameCore
{
    public class BPMSettingScriptableObject : ScriptableObject, IGameDifficultyEvaluator<BPMController>
    {
        [SerializeField] private AnimationCurve beatPrecisionRateCurve;
        [SerializeField] private AnimationCurve difficultyToBPMCurve;

        public BPMController GetGameSetting(int difficulty)
        {
            BeatPrecisionRateEvaluator evaluator = new BeatPrecisionRateEvaluator(beatPrecisionRateCurve);
            BPMController bpmController = new BPMController(1, evaluator);
            return bpmController;
        }
    }
}