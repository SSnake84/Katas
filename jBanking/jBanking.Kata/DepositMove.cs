using System;

namespace jBanking.Kata
{
    public class DepositMove : IMove
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }

        public string PrintAmount()
        {
            return Amount.ToString();
        }
        public int AmountForBalance()
        {
            return Amount;
        }
    }
}
