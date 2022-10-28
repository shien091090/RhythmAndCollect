namespace GameCore
{
    public partial class RhythmCollectGameModel : ASingleton<RhythmCollectGameModel>
    {
        public string[] currentHeadings { private set; get; }
        private HpController hpController;
        private BPMController bpmController;
        private IRhythmCollectGameEvaluator gameEvaluator;

        private void Init()
        {

        }

        public void SetCurrentHeadings(string[] newHeadings)
        {
            currentHeadings = newHeadings;
        }

        public void SetHpController(HpController _hpController)
        {
            hpController = _hpController;
        }

        public void SetBPMController(BPMController _bpmController)
        {
            bpmController = _bpmController;
        }

        public void OnClickCollectItem(RhythmCollectItem collectItem)
        {
            if (gameEvaluator == null)
                return;

            float precisionRate = 0;

            if(bpmController != null)
                precisionRate = bpmController.GetBeatPrecisionRate();

            if(hpController != null)
            {
                int hpIncrease = gameEvaluator.EvaluateAddHp(collectItem.baseHpIncrease, precisionRate, collectItem.IsCorrectClick);
                hpController.AddHp(hpIncrease);
            }

            int pointIncrease = gameEvaluator.EvaluateAddPoint(collectItem.basePoint, precisionRate, collectItem.IsCorrectClick);
            RhythmCollectGameModel_EventHandler.Instance.TriggerAddPointEvent(pointIncrease);
        }
    }
}