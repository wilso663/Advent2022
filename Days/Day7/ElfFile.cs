using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day7
{
    internal record ElfFile
    {
        public string Name { get; init; }
        public ElfDirectory Parent { get; init; }
        public int Size { get; init; }
    }
}
