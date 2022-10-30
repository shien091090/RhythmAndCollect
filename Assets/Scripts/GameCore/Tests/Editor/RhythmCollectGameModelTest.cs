using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using GameCore;

public class RhythmCollectGameModelTest
{
    private RhythmCollectGameModel model;

    [SetUp]
    public void InitTest()
    {
        model = RhythmCollectGameModel.Instance;
        model.SetCurrentHeadings(new string[] { "紅色", "香菇", "翅膀" });

        HpController hpController = new HpController(100);
        model.SetHpController(hpController);

        DummyGameEvaluator dummyEvaluator = new DummyGameEvaluator();
        BPMController bpmController = new BPMController(30, dummyEvaluator);
        model.SetBPMController(bpmController);

        model.SetEvaluator(dummyEvaluator);

        model.SetRegisterEvent(true);
    }

    [Test] //BPM30, 每秒Update一次
    [TestCase(false, true, 2, 10, 10)] //在拍子上 符合題目
    [TestCase(false, false, 2, 0, -5)] //在拍子上 不符合題目
    [TestCase(false, true, 3, 0, 0)] //不在拍子上 符合題目
    [TestCase(false, false, 3, 0, -5)] //不在拍子上 不符合題目
    [TestCase(false, true, 1, 0, 0)] //尚未第一拍
    [TestCase(true, true, 1, 0, 0)] //沒有題目
    public void IntegrationTest_ClickCollectItem(bool isCurrentHeadingIsNull, bool isMatchHeadings, int clickTimeSecond, int result_appScore, int result_addHp)
    {
        string[] attributes = null;
        if (isMatchHeadings)
            attributes = model.currentHeadings;
        else
            attributes = new string[] { "黃色" };

        if (isCurrentHeadingIsNull)
            model.SetCurrentHeadings(null);

        HpController hpController = new HpController(100);
        hpController.SetHp(50);
        model.SetHpController(hpController);

        int addScore = 0;
        RhythmCollectGameModel_EventHandler.Instance.OnAddScore += (score) => { addScore = score; };

        int addHp = 0;
        RhythmCollectGameModel_EventHandler.Instance.OnHpChangeTo += (beforeHp, afterHp) => { addHp = afterHp - beforeHp; };

        bool overFirstBeat = false;
        RhythmCollectGameModel_EventHandler.Instance.OnBeat += () => { overFirstBeat = true; };

        for (int i = 0; i < clickTimeSecond; i++)
        {
            model.UpdateTime(1);
        }

        RhythmCollectItem collectItem = new RhythmCollectItem(string.Empty, attributes, 10, 10);
        collectItem.TriggerItem(model.currentHeadings);

        Assert.AreEqual(result_appScore, addScore);
        Assert.AreEqual(result_addHp, addHp);
        Assert.AreEqual(isMatchHeadings, collectItem.IsCorrectClick);
        Assert.AreEqual(overFirstBeat, model.bpmController.isAlreadyBeatFirstTime);
    }

    [Test]
    [TestCase(false, "", true)] //題目為空
    [TestCase(true, "紅色", false)] //Item屬性為空
    [TestCase(false, "藍色", false)] //題目和Item屬性無符合
    [TestCase(false, "紅色,香菇,翅膀", true)] //題目和Item屬性全符合
    [TestCase(false, "藍色,香菇,翅膀", false)] //題目和Item屬性部分符合
    [TestCase(false, "紅色", true)] //Item屬性包含且多餘題目
    [TestCase(false, "翅膀,紅色", true)] //Item屬性包含且多餘題目(排列順序打散)
    [TestCase(false, "紅色,香菇,翅膀,火鍋", false)] //題目包含且多餘Item屬性
    [TestCase(false, "香菇,紅色,火鍋,翅膀", false)] //題目包含且多餘Item屬性(排列順序打散)
    public void LogicTest_ClickCollectItemAndCheckIsCorrect(bool attributeIsEmpty, string currentHeadingJoinString, bool result_isCorrectClick)
    {
        string[] currentHeadings = currentHeadingJoinString.Split(',');
        if (currentHeadingJoinString == string.Empty)
            currentHeadings = null;

        string[] testAttributes = null;
        if (attributeIsEmpty == false)
            testAttributes = new string[] { "紅色", "香菇", "翅膀" };

        RhythmCollectItem collectItem = new RhythmCollectItem(string.Empty, testAttributes, 1, 1);

        bool isCorrectClick = false;
        bool isEventReceived = false;
        bool isTriggered = false;

        RhythmCollectGameModel_EventHandler.Instance.OnClickCollectItem += (clickedItem) =>
        {
            isCorrectClick = clickedItem.IsCorrectClick;
            isTriggered = clickedItem.IsTriggered;
            isEventReceived = true;
        };

        Assert.AreEqual(false, isTriggered);

        collectItem.TriggerItem(currentHeadings);

        Assert.AreEqual(result_isCorrectClick, isCorrectClick);
        Assert.AreEqual(true, isEventReceived);
        Assert.AreEqual(true, isTriggered);
    }

    [Test]
    [TestCase(0, 0)] //0bpm = 每10秒 0 beats
    [TestCase(6, 1)] //6bpm = 每10秒 1 beats
    [TestCase(60, 10)] //60bpm = 每10秒 10 beats
    public void LogicTest_RepeatToBeatIn10Seconds(int bpm, int result_beatTimes)
    {
        BPMController bPMController = new BPMController(bpm, null);

        int beatTimes = 0;
        RhythmCollectGameModel_EventHandler.Instance.OnBeat += () => { beatTimes++; };

        for (int i = 0; i < 10; i++)
        {
            bPMController.Update(1);
        }

        Assert.AreEqual(result_beatTimes, beatTimes);
    }

    [Test]
    [TestCase(0, 0)] //第0秒 未打中拍子(因為第一拍還沒下)
    [TestCase(1, 0)] //第1秒 未打中拍子(因為第一拍還沒下)
    [TestCase(2, 1)] //第2秒 打中拍子
    [TestCase(3, 0)] //第3秒 未打中拍子
    [TestCase(4, 1)] //第4秒 打中拍子
    public void LogicTest_GetBeatPrecisionRate(float triggerTime, float result_precisionRate)
    {
        DummyGameEvaluator dummyEvaluator = new DummyGameEvaluator();
        BPMController bPMController = new BPMController(30, dummyEvaluator); //每2秒1beat

        int updateTimes = 4;
        int beatTimes = 0;
        RhythmCollectGameModel_EventHandler.Instance.OnBeat += () => { beatTimes++; };

        for (int timeIndex = 0; timeIndex <= updateTimes; timeIndex++)
        {
            if (timeIndex == triggerTime)
            {
                float precisionRate = bPMController.GetBeatPrecisionRate();

                Assert.AreEqual(result_precisionRate, precisionRate);
                return;
            }

            bPMController.Update(1);
        }

        Assert.Fail("No Triggered");
    }

    [Test]
    public void LogicTest_CreateHeading()
    {

    }

    [Test]
    public void LogicTest_SpawnCollectItem()
    {

    }

    [Test]
    public void LogictTest_DestroyCollectItem()
    {

    }

    [Test]
    [TestCase(100, 10, 100, false)] //滿血 +血
    [TestCase(100, -10, 90, false)] //滿血 -血
    [TestCase(100, -150, 0, true)] //滿血 -到死
    [TestCase(50, 10, 60, false)] //殘血 +血
    [TestCase(50, 60, 100, false)] //殘血 +血到超過
    [TestCase(50, 0, 50, false)] //殘血 +0
    [TestCase(50, -20, 30, false)] //殘血 -血
    [TestCase(50, -50, 0, true)] //殘血 - 血到死
    public void LogicTest_AddHp(int initHp, int addHp, int result_newHp, bool result_isDead)
    {
        HpController hp = new HpController(100, 0);
        hp.SetHp(initHp);

        int oldHp = 0;
        int newHp = 0;
        RhythmCollectGameModel_EventHandler.Instance.OnHpChangeTo += (beforeHp, currentHp) =>
        {
            oldHp = beforeHp;
            newHp = currentHp;
        };

        hp.AddHp(addHp);

        Assert.AreEqual(oldHp, initHp);
        Assert.AreEqual(result_newHp, newHp);
        Assert.AreEqual(result_isDead, hp.IsDead);
    }
}