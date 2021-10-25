using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreOOP
{
    public abstract class BaseFrame : ISubscriber
    {
        protected ISubscribable bowlingScore { get; set; }
        protected int[] Shots { get; set; } = new int[3];
        protected int ShotIndex { get; set; }
        protected int ExtraShots { get; set; }

        public BaseFrame(ISubscribable score, bool isTenthFrame)
        {
            bowlingScore = score;
            if(!isTenthFrame)
                bowlingScore.EventHandler += BowlingScore_EventHandler;
        }

        protected void BowlingScore_EventHandler(object sender, int pins)
        {
            ExtraShots--;
            if (ExtraShots < 0)
                bowlingScore.EventHandler -= BowlingScore_EventHandler;
            else
                Shots[ShotIndex++] = pins;
        }

        public virtual void Roll(int pins)
        {
            Shots[ShotIndex++] = pins;
        }

        public int GetValue() 
        {
            return Shots[0] + Shots[1] + Shots[2];
        }
    }
}
