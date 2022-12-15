using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day10
{
    internal class Day10 : Day
    {
        public override void SolvePart1()
        {
            int[] interestingCycles = new[] { 20, 60, 100, 140, 180, 220 };
            int signalStrength = GetSignalStrength(this.ReadLines()).
                Where(signal => interestingCycles.Contains(signal.cycle))
                .Select(signal => signal.value * signal.cycle)
                .Sum();
            Console.WriteLine($"Interesting cycle strengths combined; {signalStrength}");
        }

        public override void SolvePart2()
        {
            var biggin = GetSignalStrength(this.ReadLines())
                .Select(signal =>
                {
                    var sprite = signal.value;
                    var column = (signal.cycle - 1) % 40;
                    return Math.Abs(sprite - column) < 2 ? '#' : ' ';
                })
                .Chunk(40)
                .Select(line => new string(line))
                .Aggregate("", (screen, line) => screen + line + "\n");
            var bigginLines = biggin.Split("\n");
            foreach(var line in bigginLines)
            {
                Console.WriteLine($"{line}");
            }
        }

        private IEnumerable<(int cycle, int value)> GetSignalStrength(string[] input)
        {
            var (cycleCount, registerValue) = (1, 1);
            foreach(var line in input)
            {
                var parts = line.Split(" ");
                if (parts[0] == "noop")
                {
                    yield return (cycleCount++, registerValue);
                } else if (parts[0] == "addx")
                {
                    yield return (cycleCount++, registerValue);
                    yield return (cycleCount++, registerValue);
                    registerValue += int.Parse(parts[1]);
                }
            }
        }
    }
}
