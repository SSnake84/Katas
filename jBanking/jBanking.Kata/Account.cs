using System;
using System.Collections.Generic;
using System.Linq;

namespace jBanking.Kata
{
    public class Account
    {
        const int DATE_ROW_CHARS = 11;
        const int MOVE_ROW_CHARS = 10;
        const int BALANCE_ROW_CHARS = 10;

        protected List<IMove> Movements { get; set; }
        public int Balance { get; protected set; }
        
        #region constructors
        public Account()
        {
            Movements = new List<IMove>();
            Balance = 0;
        }

        public Account(List<IMove> movements)
        {
            if (movements == null)
            {
                Movements = new List<IMove>();
                Balance = 0;
                return;
            }

            this.Movements = movements;
            this.Movements.Sort((m1,m2) => m1.Amount.CompareTo(m2.Amount));
            Balance = Movements.Sum(m => m.AmountForBalance());
        }

        #endregion Constructors

        public void Deposit(int amount)
        {
            Movements.Add(new DepositMove() { Amount = amount, Date = DateTime.Now });
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Movements.Add(new WithdrawMove() { Amount = amount, Date = DateTime.Now });
            Balance -= amount;
        }

        public string PrintStatement() {
            var str = "Date".PadRight(DATE_ROW_CHARS) 
                    + "Amount".PadRight(MOVE_ROW_CHARS) 
                    + "Balance".PadRight(BALANCE_ROW_CHARS);

            int balance = this.Balance;
            foreach (IMove m in Movements)
            {
                str += Environment.NewLine + PrintStatementRow(balance, m);
                balance -= m.AmountForBalance();

            }
            return str;
        }

        protected static string PrintStatementRow(int balance, IMove move)
        {
            return 
                move.Date.ToString("dd.M.yyyy").PadRight(DATE_ROW_CHARS)
              + move.PrintAmount().PadLeft(MOVE_ROW_CHARS)
              + balance.ToString().PadLeft(BALANCE_ROW_CHARS);
        }
    }
}