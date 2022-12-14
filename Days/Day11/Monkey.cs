using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day11
{
    internal class Monkey
    {
        public Queue<long> items;
        public Func<long, long> operation;
        public int inspectedItems;
        public int modulus;
        public int passToMonkeyIfDividesBy, passToMonkeyOtherwise;
    }
}
