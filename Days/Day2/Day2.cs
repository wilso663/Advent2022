using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day2
{
    internal class Day2: Day
    {
        public override void SolvePart1()
        {
            this.Solve(new GameLogicP1());
        }

        public override void SolvePart2()
        {
            this.Solve(new GameLogicP2());
        }

        void Solve(GameLogic logic)
        {
            int score = logic.GetTotalScore(this.ReadLines());
            Console.WriteLine($"Score: {score}");
        }


    }
}
