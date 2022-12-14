using UnityEngine;

namespace GameCore
{
    public class PlayerHpAttributeScripableObject : ScriptableObject, IGameSettingGetter<HpController>
    {
        [SerializeField] private AnimationCurve difficultyToHpCurve;

        public HpController GetGameSetting(int difficulty)
        {
            return new HpController(100, 0);
        }
    }
}