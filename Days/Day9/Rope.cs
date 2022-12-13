using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day9
{
    internal class Rope
    {
        public List<Knot> Knots { get; }

        public Rope(int numKnots)
        {
            Knots = Enumerable.Range(0, numKnots).Select(k => new Knot(0, 0)).ToList();
        }

        public void PerformMoves(IEnumerable<string> moves)
        {
            foreach(var move in moves)
            {
                var instruction = move.Split(" ");
                var direction = instruction[0];
                var numSteps = int.Parse(instruction[1]);

                for(var i = 0; i < numSteps; i++)
                {
                    for(var knotNum = 0; knotNum < Knots.Count; knotNum++)
                    {
                        var knot = Knots[knotNum];
                        if (knotNum == 0)
                            knot.Move(direction);
                        else
                            knot.Follow(Knots[knotNum - 1]);
                    }
                }
            }
        }
    }
}
