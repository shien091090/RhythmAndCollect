using UnityEngine;

namespace GameCore
{
    public class HeadingCreatorScriptableObject : ScriptableObject, IGameSettingGetter<RhythmCollectGameHeadingController>
    {
        [SerializeField] private AnimationCurve diffficultyToHeadingMixCountCurve;
        [SerializeField] private AnimationCurve diffficultyToHeadingMaxCountCurve;
        [SerializeField] private AnimationCurve diffficultyToChangeHeadingFreq;

        public RhythmCollectGameHeadingController GetGameSetting(int difficulty = 0)
        {
            RhythmCollectGameHeadingCreator headingCreator = new RhythmCollectGameHeadingCreator(difficulty, diffficultyToHeadingMixCountCurve, diffficultyToHeadingMaxCountCurve);
            int freq = (int)diffficultyToChangeHeadingFreq.Evaluate(difficulty);
            RhythmCollectGameHeadingController headingController = new RhythmCollectGameHeadingController(headingCreator, freq);

            return headingController;
        }
    }
}