using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using jBowling;

namespace Bowling.Tests
{
    [TestClass]
    public class BowlingScoreTests
    {
        public void FillShots(BowlingScore score, int pins, int times)
        {
            for (int i = 0; i < times; i++)
                score.Roll(pins);
        }
        [TestMethod]
        public void Score_ShouldBe_0()
        {
            BowlingScore bowlingScore = new BowlingScore();
            FillShots(bowlingScore, 0, 21);
            bowlingScore.Score().Should().Be(0);
        }
        [TestMethod]
        public void Score_1frame_ShouldBe_9()
        {
            BowlingScore bowlingScore = new BowlingScore();
            bowlingScore.Roll(4);
            bowlingScore.Roll(5);
            FillShots(bowlingScore, 0, 19);
            bowlingScore.Score().Should().Be(9);
        }
        [TestMethod]
        public void ScoreSpare_Should_SumNextOne()
        {
            BowlingScore bowlingScore = new BowlingScore();
            bowlingScore.Roll(5);
            bowlingScore.Roll(5);
            bowlingScore.Roll(3);
            bowlingScore.Roll(2);
            FillShots(bowlingScore, 0, 17);
            bowlingScore.Score().Should().Be(18);
        }
        [TestMethod]
        public void Score6_4_Should_NotBeTreatedLikeSpare()
        {
            BowlingScore bowlingScore = new BowlingScore();
            bowlingScore.Roll(5);
            bowlingScore.Roll(4);
            bowlingScore.Roll(6);
            bowlingScore.Roll(2);
            FillShots(bowlingScore, 0, 17);
            bowlingScore.Score().Should().Be(17);
        }
        [TestMethod]
        public void Score_Strike_Should_SumNext_Two()
        {
            BowlingScore bowlingScore = new BowlingScore();
            bowlingScore.Roll(10);
            bowlingScore.Roll(4);
            bowlingScore.Roll(6);
            bowlingScore.Roll(2);
            FillShots(bowlingScore, 0, 17);
            bowlingScore.Score().Should().Be(34);
        }
        [TestMethod]
        public void Score_Three_Strikes()
        {
            BowlingScore bowlingScore = new BowlingScore();
            bowlingScore.Roll(10); // 30 + 22 + 15 + 5
            bowlingScore.Roll(10);
            bowlingScore.Roll(10);
            bowlingScore.Roll(2);
            bowlingScore.Roll(3);
            FillShots(bowlingScore, 0, 10);
            bowlingScore.Score().Should().Be(72);
        }
        [TestMethod]
        public void ScoreLastFrame_Normal()
        {
            BowlingScore bowlingScore = new BowlingScore();
            FillShots(bowlingScore, 0, 18);
            bowlingScore.Roll(8);
            bowlingScore.Roll(1);
            bowlingScore.Score().Should().Be(9);
        }
        [TestMethod]
        public void ScoreLastFrame_Spare()
        {
            BowlingScore bowlingScore = new BowlingScore();
            FillShots(bowlingScore, 0, 18);
            bowlingScore.Roll(8);
            bowlingScore.Roll(2);
            bowlingScore.Roll(5);
            bowlingScore.Score().Should().Be(20);
        }
        [TestMethod]
        public void ScoreLastFrame_Strike_Spare()
        {
            BowlingScore bowlingScore = new BowlingScore();
            FillShots(bowlingScore, 0, 18);
            bowlingScore.Roll(10);
            bowlingScore.Roll(6);
            bowlingScore.Roll(4);
            bowlingScore.Score().Should().Be(30);
        }
        [TestMethod]
        public void ScoreLastFrame_Spare_Strike()
        {
            BowlingScore bowlingScore = new BowlingScore();
            FillShots(bowlingScore, 0, 18);
            bowlingScore.Roll(4);
            bowlingScore.Roll(6);
            bowlingScore.Roll(10);
            bowlingScore.Score().Should().Be(30);
        }

    }
}