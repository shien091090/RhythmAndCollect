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
        //set heading
        //set rhythm Timing
        //set now hp state

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

        collectItem.OnTriggerMatchHeadingItem += (clickedItem) =>
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
        bPMController.OnBeat += () => { beatTimes++; };

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
        Func<float, float> EvaluateFunc = (t) => 
        {
            return t;
        };

        BPMController bPMController = new BPMController(30, EvaluateFunc); //每2秒1beat

        int updateTimes = 4;
        int beatTimes = 0;
        bPMController.OnBeat += () => { beatTimes++; };

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
    public void LogicTest_SetHp()
    {

    }
}