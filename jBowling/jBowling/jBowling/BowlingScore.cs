using System;
using System.Collections.Generic;

namespace jBowling
{
    public class BowlingScore
    {
        protected int RollIndex { get; set; } = 0; // 0 - 20
        
        public int[] Rolls { get; } = new int[21];


        public void Roll(int roll)
        {
            Rolls[RollIndex] = roll;
            RollIndex++;
        }
        public int Score() {
            short frame = 0;
            short shot = 0;

            int total = 0;

            for (var i = 0; i < Rolls.Length; i++)
            {
                switch (shot)
                {
                    case 0:
                        total += Rolls[i];
                        shot++;

                        if (Rolls[i] == 10) // Strike
                        {
                            if (Rolls.Length > i + 1)
                                total += Rolls[i + 1];
                            if (Rolls.Length > i + 2)
                                total += Rolls[i + 2];

                            if (frame != 9)
                            { 
                                frame++;
                                shot = 0;
                            }
                        }
                        break;
                    case 1:
                        total += Rolls[i];
                        shot = 0;
                        if (Rolls[i] + Rolls[i - 1] == 10) // Spare
                        {
                            if (Rolls.Length > i + 1)
                                total += Rolls[i + 1];
                        }

                        if (frame == 9)
                            shot = 2;
                        else
                            frame++;
                        break;
                    case 2:
                        total += Rolls[i];
                        break;
                }
            }
            return total;
        }
    }
}