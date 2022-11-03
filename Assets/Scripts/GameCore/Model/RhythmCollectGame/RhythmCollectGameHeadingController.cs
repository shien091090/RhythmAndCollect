using System.Linq;

namespace GameCore
{
    public class RhythmCollectGameHeadingController
    {
        private IRhythmCollectGameHeadingCreator headingCreator;
        public string[] currentHeadings { private set; get; }
        public int chagneFreq { private set; get; }
        private int changeCounter;

        public RhythmCollectGameHeadingController(IRhythmCollectGameHeadingCreator _headingCreator, int _freq = 0)
        {
            headingCreator = _headingCreator;
            chagneFreq = _freq;

            CreateNewHeadings();
        }

        public void TriggerChangeCounter()
        {
            changeCounter++;
            if (changeCounter >= chagneFreq)
            {
                CreateNewHeadings();
                changeCounter = 0;
            }
                
        }

        private void CreateNewHeadings()
        {
            if (headingCreator.GetPossibleCreateHeadingsCount() <= 1)
                return;

            string[] newHeadings = headingCreator.CreateHeadings();
            while (IsSameCurrentHeadings(newHeadings))
            {
                newHeadings = headingCreator.CreateHeadings();
            }

            currentHeadings = newHeadings;
            RhythmCollectGameModel_EventHandler.Instance.TriggerUpdateHeadingsEvent(newHeadings);
        }

        private bool IsSameCurrentHeadings(string[] targetHeadings)
        {
            if (currentHeadings == null)
                return false;

            if (currentHeadings.Length != targetHeadings.Length)
                return false;

            foreach (string headingElement in currentHeadings)
            {
                if (targetHeadings.Contains(headingElement) == false)
                    return false;
            }

            return true;
        }

    }
}