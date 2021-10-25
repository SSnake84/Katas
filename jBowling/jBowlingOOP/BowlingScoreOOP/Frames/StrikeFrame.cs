using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreOOP
{
    public class StrikeFrame : BaseFrame
    {
        public StrikeFrame(BowlingScore score, bool isTenthFrame) : base(score, isTenthFrame)
        {
            ExtraShots = 2;
            Shots[0] = 10;
            ShotIndex = 1;
        }
    }
}
