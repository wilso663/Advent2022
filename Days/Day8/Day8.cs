using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Days.Day8
{
    internal class Day8: Day
    {
        public override void SolvePart1()
        {
            Dictionary<(int x, int y), int> TreeGrid = new Dictionary<(int x, int y), int>();
            TreeGrid = populateGrid();
            int visibleTreeCount = getVisibleCount(TreeGrid);
            Console.WriteLine($"The visible tree count: {visibleTreeCount}");
        }

        public override void SolvePart2() 
        {
            Dictionary<(int x, int y), int> TreeGrid = new Dictionary<(int x, int y), int>();
            TreeGrid = populateGrid();
            var input = ReadLines();
            var totalWidth = input[0].Length;
            var totalHeight = input.Length;
            int maxScenicScore = getMaxScenicScore(TreeGrid, totalWidth, totalHeight);
            Console.WriteLine($"The max scenic score: {maxScenicScore}");
        }
        private Dictionary<(int x, int y), int> populateGrid()
        {
            Dictionary<(int x, int y), int> grid = new Dictionary<(int x, int y), int>();
            //Brain fried, got annoyed. Calling this again. May fix later, probably not.
            var input = ReadLines();
            var totalWidth = input[0].Length;
            var totalHeight = input.Length;
            for(var i = 0; i < totalHeight; i++)
            {
                var line = input[i];
                for(var j = 0; j < line.Length; j++)
                {
                    int treeValue = int.Parse(new ReadOnlySpan<char>(line[j]));
                    grid[(i,j)] = treeValue;
                }
            }
            return grid;
        }
        private int getVisibleCount(Dictionary<(int x, int y), int> treeGrid)
        {
            var visibleTrees = 0;
            foreach (var tree in treeGrid) 
            { 
                if(tree.Key.x == 0 || tree.Key.y == 0)
                    visibleTrees++;
                else
                {
                    if(treeGrid.Where(t => t.Key.x == tree.Key.x && t.Key.y < tree.Key.y).All(t => t.Value < tree.Value)) 
                    {
                        visibleTrees++;
                        continue;
                    }
                    if(treeGrid.Where(t => t.Key.y == tree.Key.y && t.Key.x < tree.Key.x).All(t => t.Value < tree.Value))
                    {
                        visibleTrees++;
                        continue;
                    }
                    if(treeGrid.Where(t => t.Key.x == tree.Key.x && t.Key.y > tree.Key.y).All(t => t.Value < tree.Value))
                    {
                        visibleTrees++;
                        continue;
                    }
                    if(treeGrid.Where(t => t.Key.y == tree.Key.y && t.Key.x > tree.Key.x).All(t => t.Value < tree.Value))
                    {
                        visibleTrees++;
                        continue;
                    }
                }
            }
            return visibleTrees;
        }

        private int getMaxScenicScore(Dictionary<(int x, int y), int> scoreMap, int width, int height)
        {
            int maxScore = 0;
            foreach (var score in scoreMap)
            {
                int left = 0;
                int top = 0;
                int right = 0;
                int bottom = 0;

                var x = score.Key.x - 1;
                var y = score.Key.y;
                while(x >= 0)
                {
                    var tree = scoreMap[(x, y)];
                    left += 1;

                    if (tree >= score.Value) 
                        break;
                    x--;
                }
                x = score.Key.x + 1;
                while(x < width)
                {
                    var tree = scoreMap[(x, y)];
                    right += 1;

                    if (tree >= score.Value)
                        break;
                    x++; 
                }
                x = score.Key.x;
                y = score.Key.y - 1;
                while(y >= 0)
                {
                    var tree = scoreMap[(x, y)];
                    top += 1;
                    if (tree >= score.Value)
                        break;
                    y--;
                }
                y = score.Key.y + 1;
                while(y < height)
                {
                    var tree = scoreMap[(x, y)];
                    bottom += 1;
                    if (tree >= score.Value)
                        break;
                    y++;
                }
                maxScore = Math.Max(maxScore, left * right * top * bottom);
            }


            return maxScore;
        }

    }
}
