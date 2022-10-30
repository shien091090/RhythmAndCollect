namespace GameCore
{
    public partial class RhythmCollectGameModel : ASingleton<RhythmCollectGameModel>
    {
        public string[] currentHeadings { private set; get; }
        private HpController hpController;
        public BPMController bpmController { private set; get; }
        private IRhythmCollectGameEvaluator gameEvaluator;

        private void Init()
        {

        }

        public void SetRegisterEvent(bool isListen)
        {
            RhythmCollectGameModel_EventHandler.Instance.OnClickCollectItem -= ClickCollectItem;

            if(isListen)
            {
                RhythmCollectGameModel_EventHandler.Instance.OnClickCollectItem += ClickCollectItem;
            }
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

        public void SetEvaluator(IRhythmCollectGameEvaluator _evaluator)
        {
            gameEvaluator = _evaluator;
        }

        private void ClickCollectItem(RhythmCollectItem collectItem)
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

            int scoreIncrease = gameEvaluator.EvaluateAddScore(collectItem.baseScore, precisionRate, collectItem.IsCorrectClick);
            RhythmCollectGameModel_EventHandler.Instance.TriggerAddScoreEvent(scoreIncrease);
        }

        public void UpdateTime(float deltaTime)
        {
            if (bpmController != null)
                bpmController.Update(deltaTime);
        }
    }
}