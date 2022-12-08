using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Days.Day1
{
    internal class Day1: Day
    {

        public override void SolvePart1()
        {
            int highestCalorieCount = this.GetTopCalorieCounts(1);

            Console.WriteLine($"Highest calorie count: {highestCalorieCount}");
        }

        public override void SolvePart2() 
        {
            int highestThreeCalorieCounts = this.GetTopCalorieCounts(3);
            Console.WriteLine($"Highest 3 calories combined: {highestThreeCalorieCounts}");
        }

        int GetTopCalorieCounts(int numberOfCounts)
        {
            IOrderedEnumerable<int> highestCalorieCounts = GetCaloriesPerElf()
               .OrderByDescending(num => num);
            int index = 0;
            int sum = 0;
            foreach(int calorieCount in highestCalorieCounts.Take(numberOfCounts))
            {
                sum += calorieCount;
                ++index;
            }
            return sum;
        }

        IEnumerable<int> GetCaloriesPerElf()
        {
            return CountCaloriesPerElf(this.ReadLines());
        }

        static IEnumerable<int> CountCaloriesPerElf(IEnumerable<string> lines) 
        {
            int currentCalorieCount = 0;
            foreach(string line in lines) 
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    yield return currentCalorieCount;
                    currentCalorieCount = 0;
                    continue;
                }
                currentCalorieCount += int.Parse(line);
            }
        }
    }
}
