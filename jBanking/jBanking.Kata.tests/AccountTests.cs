using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using FluentAssertions;

namespace jBanking.Kata
{
    public class AccountTests
    {
        #region Reflection Helpers
        public T GetReflectedValue<T>(object obj, string prop)
        {
            return (T)obj.GetType().GetProperty(prop, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj, null);
        }

        public string InvokePrintStatementRow(int balance, IMove move)
        {
            return typeof(Account).GetMethod("PrintStatementRow", BindingFlags.Static | BindingFlags.NonPublic)
            .Invoke(null, new object[] { balance, move }) as string;
        }
        #endregion Reflection Helpers

        #region Constructors
        [Fact]
        public void ConstructAccountWithoutArguments_ShouldBeInitialized()
        {
            // Arrange
            int expectedBalance = 0;
            int actualMovements;
            int expectedMovementsCount = 0;
            // Act
            var acc = new Account();
            actualMovements = GetReflectedValue<List<IMove>>(acc, "Movements").Count;

            // Assert
            acc.Balance.Should().Be(expectedBalance);
            actualMovements.Should().Be(expectedMovementsCount);
        }
        [Fact]
        public void ConstructAccountWithoNullMovements_ShouldBeInitialized()
        {
            // Arrange
            int expectedBalance = 0;
            int actualMovements;
            int expectedMovementsCount = 0;
            // Act
            var acc = new Account(null);
            actualMovements = GetReflectedValue<List<IMove>>(acc, "Movements").Count;

            // Assert
            acc.Balance.Should().Be(expectedBalance);
            actualMovements.Should().Be(expectedMovementsCount);
        }
        [Fact]
        public void ConstructAccountWithMovements_ShouldBeInitializedAndOrdered()
        {
            // Arrange
            int expectedBalance = -450;
            List<IMove> actualMovements;
            int expectedMovementsCount = 5;
            DateTime dateExpected = new DateTime(2021, 10, 21);
            // Act
            var acc = new Account(
                new List<IMove>(){
                    new WithdrawMove(){ Date = new DateTime(2021,10,10), Amount = 150},
                    new WithdrawMove(){ Date = new DateTime(2021,10,08), Amount = 300},
                    new DepositMove(){ Date = new DateTime(2021,10,01), Amount = 150 },
                    new WithdrawMove(){ Date = new DateTime(2021,10,15), Amount = 100},
                    new WithdrawMove(){ Date = new DateTime(2021,10,21), Amount =  50},
                }
            );
            actualMovements = GetReflectedValue<List<IMove>>(acc, "Movements");

            // Assert
            acc.Balance.Should().Be(expectedBalance);
            actualMovements.Count.Should().Be(expectedMovementsCount);
            actualMovements.First().Date.Should().Be(dateExpected);
        }
        #endregion Constructors

        #region Deposit & Withdraw
        [Fact]
        public void Deposit_ShouldIncreaseBalanceAndAddMovement()
        {
            // Arrange
            int expectedBalance = 100;
            int actualMovements;
            int expectedMovementsCount = 1;
            // Act
            var acc = new Account();
            acc.Deposit(100);
            actualMovements = GetReflectedValue<List<IMove>>(acc, "Movements").Count;

            // Assert
            acc.Balance.Should().Be(expectedBalance);
            actualMovements.Should().Be(expectedMovementsCount);
        }
        [Fact]
        public void Withdraw_ShouldDecreaseBalanceAndAddMovement()
        {
            // Arrange
            int expectedBalance = -100;
            int actualMovements;
            int expectedMovementsCount = 1;
            // Act
            var acc = new Account();
            acc.Withdraw(100);
            actualMovements = GetReflectedValue<List<IMove>>(acc, "Movements").Count;

            // Assert
            acc.Balance.Should().Be(expectedBalance);
            actualMovements.Should().Be(expectedMovementsCount);
        }
        #endregion Deposit & Withdraw

        #region PrintStatements
        [Fact]
        protected void PrintStatementRow_ShouldManageDeposit()
        {
            // Arrange
            string actualRow;
            string expectedRow = "20.8.2021        1500      3000";

            // Act
            actualRow = InvokePrintStatementRow(3000, new DepositMove() { Amount = 1500, Date = new DateTime(2021, 8,20) });

            // Assert
            actualRow.Should().Be(expectedRow);
        }
        [Fact]
        protected void PrintStatementRow_ShouldManageWithdraw()
        {
            // Arrange
            string actualRow;
            string expectedRow = "20.8.2021       -1500      3000";

            // Act
            actualRow = InvokePrintStatementRow(3000, new WithdrawMove() { Amount = 1500, Date = new DateTime(2021, 8, 20) });

            // Assert
            actualRow.Should().Be(expectedRow);
        }
        [Fact]
        protected void PrintStatement_ShouldReturnOnlyHeaders()
        {
            // Arrange
            string actualRow;
            string expectedRow = "20.8.2021        1500      3000";

            // Act
            actualRow = InvokePrintStatementRow(3000, new DepositMove() { Amount = 1500, Date = new DateTime(2021, 8, 20) });

            // Assert
            actualRow.Should().Be(expectedRow);
        }
        [Fact]
        public void PrintStatement_ShouldReturnTwoRows()
        {
            // Arrange
            var expectedStatement = "Date       Amount    Balance   " + Environment.NewLine
                                  + "24.8.2021       -1000      1500" + Environment.NewLine
                                  + "21.8.2021        2500      2500";
            string actualStatement;
            // Act
            var acc = new Account(new List<IMove> {
                new DepositMove { Date = new DateTime(2021,8,21), Amount = 2500 },
                new WithdrawMove { Date = new DateTime(2021,8,24), Amount = 1000 },
            });
            
            actualStatement = acc.PrintStatement();

            // Assert
            actualStatement.Should().Be(expectedStatement);
        }
        #endregion PrintStatements
    }
}