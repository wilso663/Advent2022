using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day12
{
    internal class Day12 : Day
    {
        Symbol startSymbol = new Symbol('S');
        Symbol goalSymbol = new Symbol('E');
        Elevation lowestElevation = new Elevation('a');
        Elevation highestElevation = new Elevation('z');
        public override void SolvePart1()
        {
            var input = this.ReadLines();
            int distance = GetInterestPoints(input).Single(p => p.symbol == startSymbol).distanceFromGoal;
            Console.WriteLine($"The combined distance from start to finish: {distance}");
        }

        public override void SolvePart2()
        {
            var input = this.ReadLines();
            int minDistance = GetInterestPoints(input).Where(p => p.elevation == lowestElevation).Select(p => p.distanceFromGoal).Min();
            Console.WriteLine($"The min distance: {minDistance}");
        }


        private IEnumerable<InterestPoint> GetInterestPoints(string[] inputLines)
        {
            var map = ParseMap(inputLines);
            var goal = map.Keys.Single(p => map[p] == goalSymbol);

            var interestPointsCoords = new Dictionary<Coord, InterestPoint>()
            {
                {goal, new InterestPoint(goalSymbol, GetElevation(goalSymbol), 0) }
            };

            var queue = new Queue<Coord>();
            queue.Enqueue(goal);
            while (queue.Any())
            {
                var currentCoord = queue.Dequeue();
                var currentInterestPoint = interestPointsCoords[currentCoord];

                foreach(var nextCoord in Neighbours(currentCoord).Where(map.ContainsKey))
                {
                    if (interestPointsCoords.ContainsKey(nextCoord))
                        continue;

                    var nextSymbol = map[nextCoord];
                    var nextElevation = GetElevation(nextSymbol);

                    if(currentInterestPoint.elevation.value - nextElevation.value <= 1)
                    {
                        interestPointsCoords[nextCoord] = new InterestPoint(
                           symbol: nextSymbol,
                           elevation: nextElevation,
                           distanceFromGoal: currentInterestPoint.distanceFromGoal + 1 
                        );
                        queue.Enqueue(nextCoord);
                    }
                }
            }
            return interestPointsCoords.Values;
        }
        private Elevation GetElevation(Symbol symbol)
        {
            return symbol.value switch
            {
                'S' => lowestElevation,
                'E' => highestElevation,
                _ => new Elevation(symbol.value)
            };
        }

        private ImmutableDictionary<Coord, Symbol> ParseMap(string[] inputLines)
        {
            return (
                from y in Enumerable.Range(0, inputLines.Length)
                from x in Enumerable.Range(0, inputLines[0].Length)
                select new KeyValuePair<Coord, Symbol>(
                    new Coord(x, y), new Symbol(inputLines[y][x])
                )
            ).ToImmutableDictionary();
        }
        private IEnumerable<Coord> Neighbours(Coord coord) { 
           return new[] { 
                coord with { x = coord.x + 1},
                coord with { x = coord.x - 1},
                coord with { y = coord.y + 1},
                coord with { y = coord.y - 1},
            };
        }
    }
}
