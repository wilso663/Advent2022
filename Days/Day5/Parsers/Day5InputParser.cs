using AdventOfCode2022.Days.Day5.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day5.Parsers
{
    class Day5InputParser
    {
        internal Day5Input Parse(string[] lines)
        {
            var crateLines = new List<string>();
            while (lines[crateLines.Count].Contains('['))
            {
                crateLines.Add(lines[crateLines.Count]);
            }
            var crateStackParser = new CrateStackParser();
            int numberOfCrates = crateStackParser.ParseNumberOfStacks(lines[crateLines.Count]);
            Stack<Crate>[] stacks = crateStackParser.ParseStacks(crateLines.ToArray(), numberOfCrates);

            var movementOperationParser = new MovementOperationParser();
            MovementOperation[] movementOperations = lines.Skip(crateLines.Count + 2)
                .Select(movementOperationParser.ParseMovementOperation)
                .ToArray();
            return new Day5Input(stacks, movementOperations);
        }
    }
}
