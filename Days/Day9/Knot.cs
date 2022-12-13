using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day9
{
    internal class Knot
    {
        private int X { get; set; }
        private int Y { get; set; }
        public Knot(int x, int y) 
        {
            X = x;
            Y = y;

            visited.Add((x, y));
        }

        public HashSet<(int, int)> visited { get; } = new HashSet<(int,int)>();

        public void Follow(Knot leadKnot)
        {
            if (Math.Abs(leadKnot.X - X) < 2 && Math.Abs(leadKnot.Y - Y) < 2) return;
            if(leadKnot.X == X)
            {
                if (leadKnot.Y > Y) Y++;
                else Y--;
            }
            else if(leadKnot.Y == Y)
            {
                if (leadKnot.X > X) X++;
                else X--;
            }
            else
            {
                if (leadKnot.X > X) X++;
                else X--;

                if (leadKnot.Y > Y) Y++;
                else Y--;
            }
            visited.Add((X, Y));
        }

        public void Move(string direction)
        {
            if (direction == "L") X--;
            else if (direction == "R") X++;
            else if (direction == "U") Y++;
            else if (direction == "D") Y--;

            visited.Add((X, Y));
        }
    }
}
