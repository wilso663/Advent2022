using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day14
{
    internal class Day14 : Day
    {

        private ISet<Coord> griddy = new HashSet<Coord>();
        private int floor_y = 0;
        private int min_x = 0;
        private int max_x = 0;
        private readonly string coordRegex = @"(\d+),(\d+)";

        public override void SolvePart1()
        {
            var input = this.ReadLines();
            PopulateGriddyAndBounds(input, coordRegex);
            var restingSandCount = GetRestingSandCount();
            Console.WriteLine($"Amount of sand at rest: {restingSandCount}");
        }

        public override void SolvePart2()
        {
            var input = this.ReadLines();
            PopulateGriddyAndBounds(input, coordRegex);
            AddFloorToGriddy();
            var restingSandCount = GetRestingSandCount();
            Console.WriteLine($"Amount of sand at rest: {restingSandCount}");
        }

        private int GetRestingSandCount()
        {
            int count = 0;
            Coord sandCoord = new(500,0);
            while(sandCoord.y < floor_y)
            {
                if(!griddy.Contains(new Coord(sandCoord.x, sandCoord.y + 1)))
                {
                    sandCoord.y += 1;
                } else if(!griddy.Contains(new Coord(sandCoord.x - 1, sandCoord.y + 1)))
                {
                    sandCoord.x -= 1;
                    sandCoord.y += 1;
                } else if(!griddy.Contains(new Coord(sandCoord.x + 1, sandCoord.y + 1)))
                {
                    sandCoord.x += 1;
                    sandCoord.y += 1;
                } else
                {
                    count++;
                    griddy.Add(sandCoord);
                    if (sandCoord.y == 0)
                    {
                        break;
                    }
                    sandCoord = new Coord(500, 0);
                }
            }
            return count;
        }

        private void AddFloorToGriddy()
        {
            int floor = floor_y + 2;
            //arbitrary big number coming in. The floor min x and max x are supposed to be infinite.
            int left_bound = min_x - 10000; 
            int right_bound = max_x + 10000;
            for(int i = left_bound; i <= right_bound; i++)
            {
                griddy.Add(new Coord(i, floor));
            }
            floor_y = floor;
            min_x = left_bound;
            max_x = right_bound;
        }
        private void PopulateGriddyAndBounds(string[] lines, string regexPattern)
        {
 
            foreach (var line in lines)
            {
                Coord currentCoord = new Coord(0, 0);
                var matches = Regex.Matches(line, regexPattern).Cast<Match>().Select(match => match.Value).ToList();
                foreach (var m in matches)
                {
                    var nums = m.Split(',');
                    //should always return 2 matches --big hope, try parse? no thanks
                    var match_x = int.Parse(nums[0]);
                    var match_y = int.Parse(nums[1]);

                    if (currentCoord.x == 0 && currentCoord.y == 0)
                    {
                        currentCoord.x = match_x;
                        currentCoord.y = match_y;
                    }
                    else
                    {
                        if (match_x == currentCoord.x)
                        {
                            if (match_y < currentCoord.y)
                            {
                                for (int i = match_y; i <= currentCoord.y; i++)
                                {
                                    griddy.Add(new Coord(match_x, i));
                                }
                            }
                            else
                            {
                                for (int i = currentCoord.y; i <= match_y; i++)
                                {
                                    griddy.Add(new Coord(match_x, i));
                                }
                            }
                        }
                        else
                        {
                            if (match_x < currentCoord.x)
                            {
                                for (int i = match_x; i <= currentCoord.x; i++)
                                    griddy.Add(new Coord(i, match_y));
                            }
                            else
                            {
                                for (int i = currentCoord.x; i <= match_x; i++)
                                    griddy.Add(new Coord(i, match_y));
                            }
                        }
                        currentCoord.x = match_x;
                        currentCoord.y = match_y;
                    }
                    floor_y = Math.Max(floor_y, match_y);
                    min_x = Math.Max(min_x, match_x);
                    max_x = Math.Max(max_x, match_x);
                }
            }
        }
        TryParse LineParser(string line)
        {
            bool match(string pattern, out string arg)
            {
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    arg = match.Groups[match.Groups.Count - 1].Value;
                    return true;
                }
                else
                {
                    arg = "";
                    return false;
                }
            }
            return match;
        }

        delegate bool TryParse(string pattern, out string arg);
    }
}
