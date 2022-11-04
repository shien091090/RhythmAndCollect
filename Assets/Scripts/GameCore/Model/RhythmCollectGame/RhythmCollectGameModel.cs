using System;
using System.Collections.Generic;

namespace GameCore
{
    public partial class RhythmCollectGameModel : ASingleton<RhythmCollectGameModel>
    {
        private HpController hpController;
        public BPMController bpmController { private set; get; }
        private IRhythmCollectGameEvaluator gameEvaluator;
        private RhythmCollectItemSpawner collectItemSpawner;
        private RhythmCollectGameHeadingController headingController;
        private GameSettingManager gameSettingManager;
        private Dictionary<Type, IGameInit> gameInitData;

        public int GetCurrentAliveCollectItemCount
        {
            get
            {
                if (collectItemSpawner == null)
                    return 0;
                else
                    return collectItemSpawner.GetCurrentAliveItemCount;
            }
        }

        public int GetClickedCollectItemCount
        {
            get
            {
                if (collectItemSpawner == null)
                    return 0;
                else
                    return collectItemSpawner.GetClickedItemCount;
            }
        }

        public string[] GetCurrentHeadings
        {
            get
            {
                if (headingController == null)
                    return null;
                else
                    return headingController.currentHeadings;
            }
        }

        public void Init()
        {
            gameSettingManager = new GameSettingManager();
            HpController hpController = gameSettingManager.GetSetting<HpController>(1);
            BPMController bpmController = gameSettingManager.GetSetting<BPMController>(1);
            IRhythmCollectGameEvaluator gameEvaluator = gameSettingManager.GetSetting<IRhythmCollectGameEvaluator>(1);
            RhythmCollectItemSpawner collectItemSpawner = CreateSettingForInit(gameSettingManager.GetSetting<RhythmCollectItemSpawner>());
            RhythmCollectGameHeadingController headingController = gameSettingManager.GetSetting<RhythmCollectGameHeadingController>(1);

            SetRegisterEvent(true);

            SetHpController(hpController);
            SetBPMController(bpmController);
            SetGameEvaluator(gameEvaluator);
            SetCollectItemSpawner(collectItemSpawner);
            SetHeadingCreator(headingController);
        }

        private T CreateSettingForInit<T>(T setting) where T : IGameInit
        {
            if (gameInitData == null)
                gameInitData = new Dictionary<Type, IGameInit>();

            gameInitData[typeof(T)] = setting;

            return setting;
        }

        public void SetRegisterEvent(bool isListen)
        {
            SetInitSettingBindEvent(isListen);

            RhythmCollectGameModel_EventHandler.Instance.OnClickCollectItem -= ClickCollectItem;

            if (isListen)
            {
                RhythmCollectGameModel_EventHandler.Instance.OnClickCollectItem += ClickCollectItem;
            }
        }

        private void SetInitSettingBindEvent(bool isListen)
        {
            if (gameInitData == null || gameInitData.Count <= 0)
                return;

            foreach (KeyValuePair<Type, IGameInit> initInfoPair in gameInitData)
            {
                IGameInit initInfo = initInfoPair.Value;

                if (isListen)
                    initInfo.BindEvent();
                else
                    initInfo.CancelBindEvent();
            }
        }

        public void SetHpController(HpController _hpController)
        {
            hpController = _hpController;
        }

        public void SetBPMController(BPMController _bpmController)
        {
            bpmController = _bpmController;
        }

        public void SetGameEvaluator(IRhythmCollectGameEvaluator _evaluator)
        {
            gameEvaluator = _evaluator;
        }

        public void SetCollectItemSpawner(RhythmCollectItemSpawner _spawner)
        {
            collectItemSpawner = _spawner;
        }

        public void SetHeadingCreator(RhythmCollectGameHeadingController _headingController)
        {
            headingController = _headingController;
        }

        private void ClickCollectItem(RhythmCollectItem collectItem)
        {
            if (gameEvaluator == null)
                return;

            float precisionRate = 0;

            if (bpmController != null)
                precisionRate = bpmController.GetBeatPrecisionRate();

            if (hpController != null)
            {
                int hpIncrease = gameEvaluator.EvaluateAddHp(collectItem.GetBaseHpIncrease, precisionRate, collectItem.IsCorrectClick);
                hpController.AddHp(hpIncrease);
            }

            int scoreIncrease = gameEvaluator.EvaluateAddScore(collectItem.GetBaseScore, precisionRate, collectItem.IsCorrectClick);
            RhythmCollectGameModel_EventHandler.Instance.TriggerAddScoreEvent(scoreIncrease);
        }

        public void UpdateTime(float deltaTime)
        {
            if (bpmController != null)
                bpmController.Update(deltaTime);

            if (collectItemSpawner != null)
                collectItemSpawner.Update(deltaTime);
        }
    }
}