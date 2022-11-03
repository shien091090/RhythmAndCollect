namespace GameCore
{
    public interface IGameDifficultyEvaluator<T>
    {
        T GetGameSetting(int difficulty);
    }
}
