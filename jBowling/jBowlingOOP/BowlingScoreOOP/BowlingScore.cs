using System;
using System.Collections.Generic;

namespace BowlingScoreOOP
{
    public class BowlingScore : ISubscribable
    {
        public List<BaseFrame> Frames { get; set; } = new List<BaseFrame>();

        private int LastShot { get; set; }
        public short ShotIndex { get; set; }

        public event EventHandler<int> EventHandler;
        public void NotifyObservers(int pins)
        {
            if (EventHandler != null)
                EventHandler(this, pins);
        }


        public void Roll(int roll)
        {
            NotifyObservers(roll);

            bool isTenthFrame = Frames.Count == 10;
            if (ShotIndex == 0 && roll == 10)
            {
                if (isTenthFrame)
                {
                    Frames[9].Roll(10);
                    ShotIndex++;
                }
                else
                    Frames.Add(new StrikeFrame(this, isTenthFrame));
            }
            else if (roll + LastShot == 10)
            {
                if (Frames.Count == 10)
                {
                    Frames[9].Roll(roll);
                    ShotIndex++;
                }
                else
                {
                    Frames.Add(new SpareFrame(this, isTenthFrame));
                    ShotIndex = 0;
                }
            }
            else
            {
                if (ShotIndex == 0)
                {
                    LastShot = roll;
                    ShotIndex++;
                }
                else
                {
                    BaseFrame f = new NormalFrame(this, isTenthFrame);
                    f.Roll(LastShot);
                    f.Roll(roll);
                    Frames.Add(f);
                    LastShot = 0;
                    ShotIndex = 0;
                }
            }
        }

        public int Score()
        {
            int ret = 0;
            foreach (BaseFrame frame in Frames)
                ret += frame.GetValue();
            return ret;
        }
    }
}
