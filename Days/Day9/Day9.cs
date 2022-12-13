using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day9
{
    internal class Day9 : Day
    {
        public override void SolvePart1()
        {
            var input = this.ReadLines();

            var rope = new Rope(2);
            rope.PerformMoves(input);
            var tail = rope.Knots.Last();
            Console.WriteLine($"Tail visited {tail.visited.Count}");
        }

        public override void SolvePart2()
        {

            var input = this.ReadLines();
            var rope = new Rope(10);
            rope.PerformMoves(input);
            var tail = rope.Knots.Last();
            Console.WriteLine($"Tail visited {tail.visited.Count}");
        }



     
    }
}
