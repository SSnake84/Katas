using System;
using Xunit;
using FluentAssertions;

namespace jBanking.Kata
{
    public class MoveTests
    {
        [Fact]
        public void Move_ShouldPrintNegativeValues()
        {
            // Arrange
            string expected = "-1500";
            string actual;

            // Act
            var move = new WithdrawMove() { Amount = 1500, Date = DateTime.Now };
            actual = move.PrintAmount();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Move_ShouldPrintPositiveValues()
        {
            // Arrange
            string expected = "1500";
            string actual;

            // Act
            var move = new DepositMove() { Amount = 1500, Date = DateTime.Now };
            actual = move.PrintAmount();

            // Assert
            actual.Should().Be(expected);
        }

    }
}
