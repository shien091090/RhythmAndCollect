namespace GameCore
{
    public class DummyHeadingCreator : IRhythmCollectGameHeadingCreator
    {
        private bool isOnlyOneResultForDummy;
        private int creatorIndex;

        public DummyHeadingCreator(bool _isOnlyOneResult = false)
        {
            isOnlyOneResultForDummy = _isOnlyOneResult;
            creatorIndex = 0;
        }

        public string[] CreateHeadings()
        {
            if (isOnlyOneResultForDummy)
                return new string[] { "紅色", "香菇", "翅膀" };
            else
            {
                string[] resultHeadings = null;

                if (creatorIndex == 0)
                    resultHeadings = new string[] { "紅色", "長的" };
                else if (creatorIndex == 1)
                    resultHeadings = new string[] { "紅色", "木頭", "翅膀" };
                else
                    resultHeadings = new string[] { "白色", "香菇" };

                creatorIndex++;
                if (creatorIndex > 3)
                    creatorIndex = 0;

                return resultHeadings;
            }
        }

        public int GetPossibleCreateHeadingsCount()
        {
            if (isOnlyOneResultForDummy)
                return 1;
            else
                return 3;
        }
    }
}