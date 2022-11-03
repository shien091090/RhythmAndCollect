namespace GameCore
{
    public interface IGameSettingGetter<T>
    {
        T GetGameSetting(int difficulty = 0);
    }
}
