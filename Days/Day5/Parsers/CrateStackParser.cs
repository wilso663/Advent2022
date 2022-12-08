using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2022.Days.Day5.DTOs;

namespace AdventOfCode2022.Days.Day5.Parsers
{
    class CrateStackParser
    {
        internal int ParseNumberOfStacks(string crateNumberLine)
        {
            char lastNumberChar = crateNumberLine.Where(char.IsNumber).Last();
            return int.Parse(new ReadOnlySpan<char>(lastNumberChar));
        }

        internal Stack<Crate>[] ParseStacks(string[] crateLines, int numberOfCrates)
        {
            var stacks = new Stack<Crate>[numberOfCrates];
            foreach(string crateLine in crateLines.Reverse()) 
            { 
                for(int i = 1; i < crateLine.Length; i += 4)
                {
                    int stackID = (i - 1) / 4;
                    if (char.IsWhiteSpace(crateLine[i]) || stacks.Length <= stackID)
                        continue;

                    var crate = new Crate(crateLine[i]);

                    if (stacks[stackID] == null)
                        stacks[stackID] = new Stack<Crate>();

                    stacks[stackID].Push(crate);
                }
            }
            return stacks;
        }
    }
}
