using System.Linq;

namespace GameCore
{
    public class RhythmCollectGameHeadingController
    {
        private IRhythmCollectGameHeadingCreator headingCreator;
        public string[] currentHeadings { private set; get; }
        private UpdateTimer changeHeadingTimer;

        public RhythmCollectGameHeadingController(IRhythmCollectGameHeadingCreator _headingCreator, int _freq = 0)
        {
            headingCreator = _headingCreator;
            changeHeadingTimer = new UpdateTimer(_freq);

            CreateNewHeadings();

            changeHeadingTimer.OnTriggerTimer += CreateNewHeadings;
        }

        public void SetHeadingChangeFreq(int _freq)
        {
            changeHeadingTimer.SetResetTimeThreshold(_freq);
        }

        public void CounterChangeTick()
        {
            changeHeadingTimer.Update(1);
        }

        private void CreateNewHeadings()
        {
            if (headingCreator.GetPossibleCreateHeadingsCount() <= 0)
                return;

            bool isOnlyOnePossible = headingCreator.GetPossibleCreateHeadingsCount() == 1;
            string[] newHeadings = headingCreator.CreateHeadings();

            if (isOnlyOnePossible && IsSameCurrentHeadings(newHeadings))
                return;

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