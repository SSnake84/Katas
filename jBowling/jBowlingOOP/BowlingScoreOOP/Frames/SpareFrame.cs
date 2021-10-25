using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreOOP
{
    public class SpareFrame : BaseFrame
    {
        public SpareFrame(BowlingScore score, bool isTenthFrame) : base(score, isTenthFrame)
        {
            ExtraShots = 1;
            Shots[0] = 10;
            ShotIndex = 1;
        }
    }
}
