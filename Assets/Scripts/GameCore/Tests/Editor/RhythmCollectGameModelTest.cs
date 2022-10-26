using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
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

        collectItem.OnTriggerMatchHeadingItem += (isMatch) =>
        {
            isCorrectClick = isMatch;
            isEventReceived = true;
        };

        Assert.AreEqual(false, collectItem.isTriggered);

        collectItem.TriggerItem(currentHeadings);

        Assert.AreEqual(result_isCorrectClick, isCorrectClick);
        Assert.AreEqual(true, isEventReceived);
        Assert.AreEqual(true, collectItem.isTriggered);
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