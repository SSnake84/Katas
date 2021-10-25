using System;

namespace jBanking.Kata
{
    public interface IMove
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }

        string PrintAmount();
        int AmountForBalance();
    }
}