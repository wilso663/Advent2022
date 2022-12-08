using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day3
{
    internal class Day3 : Day
    {
        public override void SolvePart1()
        {
            int sum = 0;
            foreach (string line in ReadLines())
            {
                IEnumerable<char> itemsInCompartment1 = line.Take(line.Length / 2);
                IEnumerable<char> itemsInCompartment2 = line.Skip(line.Length / 2);
                IEnumerable<char> itemsInBothCompartments = itemsInCompartment1.Intersect(itemsInCompartment2);
                sum += GetSumOfPriorities(itemsInBothCompartments);
            }

            Console.WriteLine($"Sum: {sum}");
        }

        public override void SolvePart2()
        {
            int sum = 0;
            int numElves = 3;
            string[] elfInventories = this.ReadLines();
            for(int i =0; i < elfInventories.Length; i += numElves)
            {
                IEnumerable<char> intersectOfItems = elfInventories[i];
                for(int j = 1; j < numElves; ++j)
                {
                    intersectOfItems = intersectOfItems.Intersect(elfInventories[i + j]);
                }
                char badge = intersectOfItems.Single();

                sum += this.GetPriority(badge);
            }
            Console.WriteLine($"Sum: {sum}");
        }

        char[] priority { get; } = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        int GetSumOfPriorities(IEnumerable<char> items)
        {
            return items.Select(GetPriority).Sum();
        }

        int GetPriority(char item)
        {
            return Array.IndexOf(priority, item);
        }
    }
}
