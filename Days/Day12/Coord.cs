using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day12
{
    internal record struct Coord
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coord(int x, int y)
        {
            this.x = x; 
            this.y = y;
        }

        public Coord()
        {
            x = 0;
            y = 0;
        }
    }
}
