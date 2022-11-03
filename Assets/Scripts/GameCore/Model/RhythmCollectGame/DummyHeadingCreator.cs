namespace GameCore
{
    public class DummyHeadingCreator : IRhythmCollectGameHeadingCreator
    {
        private bool isOnlyOneResultForDummy;

        public DummyHeadingCreator(bool _isOnlyOneResult = false)
        {
            isOnlyOneResultForDummy = _isOnlyOneResult;
        }

        public string[] CreateHeadings()
        {
            if (isOnlyOneResultForDummy)
                return new string[] { "紅色", "香菇", "翅膀" };

            System.Random dice = new System.Random();
            int index = dice.Next(0, 3);

            if (index == 0)
                return new string[] { "紅色", "長的" };
            else if (index == 1)
                return new string[] { "紅色", "木頭", "翅膀" };
            else if (index == 2)
                return new string[] { "白色", "香菇" };
            else
                return new string[0];
        }

        public int GetPossibleCreateHeadingsCount()
        {
            if (isOnlyOneResultForDummy)
                return 0;
            else
                return 3;
        }
    }
}