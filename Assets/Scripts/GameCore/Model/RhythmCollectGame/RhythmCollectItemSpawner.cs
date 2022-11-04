﻿using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class RhythmCollectItemSpawner
    {
        private UpdateTimer spawnTimer;
        private ICollectItemSpawnFrequency spawnFreqController;
        private ICollectItemSpawnAttribute spawnAttributeController;
        private int currentBpm;
        private string[] currentHeadings;
        private List<RhythmCollectItem> collectItemRecordList;

        private List<RhythmCollectItem> GetCurrentAliveItemList
        {
            get
            {
                if (collectItemRecordList.Count <= 0)
                    return new List<RhythmCollectItem>();

                return collectItemRecordList.Where(x => x.IsDisappeared == false).ToList();
            }
        }

        public int GetCurrentAliveItemCount
        {
            get
            {
                return GetCurrentAliveItemList.Count;
            }
        }

        public int GetClickedItemCount
        {
            get
            {
                if (collectItemRecordList.Count <= 0)
                    return 0;
                else
                    return collectItemRecordList.Where(x => x.IsTriggered).Count();
            }
        }

        public RhythmCollectItemSpawner(ICollectItemSpawnFrequency _spawnFreq, ICollectItemSpawnAttribute _spawnAttribute)
        {
            collectItemRecordList = new List<RhythmCollectItem>();
            spawnTimer = new UpdateTimer(0);
            spawnFreqController = _spawnFreq;
            spawnAttributeController = _spawnAttribute;
            //currentBpm = _initBpm;
            //currentHeadings = _initHeadings;

            UpdateSpawnFreq();

            spawnTimer.OnTriggerTimer += OnTriggerSpawnTimer;
        }

        private void UpdateSpawnFreq()
        {
            float freq = spawnFreqController.GetSpawnFreq(GetCurrentAliveItemCount, currentBpm);
            spawnTimer.SetResetTimeThreshold(freq);
        }

        public void Update(float deltaTime)
        {
            spawnTimer.Update(deltaTime);
        }

        private void SpawnNewCollectItem()
        {
            RhythmCollectItemAttribute attributeInfo = spawnAttributeController.GetCollectItemAttribute(GetCurrentAliveItemList, currentHeadings);
            RhythmCollectItem newCollectItem = new RhythmCollectItem(attributeInfo);
            collectItemRecordList.Add(newCollectItem);

            RhythmCollectGameModel_EventHandler.Instance.TriggerSpawnCollectItemEvent(newCollectItem);
        }

        private void OnTriggerSpawnTimer()
        {
            SpawnNewCollectItem();
            UpdateSpawnFreq();
        }
    }

}