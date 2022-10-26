namespace GameCore
{
    public partial class RhythmCollectGameModel : ASingleton<RhythmCollectGameModel>
    {
        public string[] currentHeadings { private set; get; }

        private void Init()
        {

        }

        public void SetCurrentHeadings(string[] newHeadings)
        {
            currentHeadings = newHeadings;
        }
    }
}