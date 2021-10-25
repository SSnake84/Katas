using System;

namespace BowlingScoreOOP
{
    public interface ISubscribable
    {
        public event EventHandler<int> EventHandler;
    }
}