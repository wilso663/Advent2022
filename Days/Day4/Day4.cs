using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day4
{
    internal class Day4: Day
    {
        private readonly record struct Assignment(int lowerBound, int upperBound);
        private readonly record struct AssignmentPair(Assignment firstAssign, Assignment secondAssign);

        private bool hasOverlap(AssignmentPair pair)
        {
            if (pair.firstAssign.lowerBound <= pair.secondAssign.lowerBound &&
                pair.firstAssign.upperBound >= pair.secondAssign.lowerBound)
                return true;
            
            if (pair.secondAssign.lowerBound <= pair.firstAssign.lowerBound &&
                pair.secondAssign.upperBound >= pair.firstAssign.lowerBound)
                return true;

            return false;
        }

        private bool isFullyInPair(AssignmentPair pair)
        {
            if (pair.firstAssign.lowerBound <= pair.secondAssign.lowerBound &&
                pair.firstAssign.upperBound >= pair.secondAssign.upperBound)
                return true;

            if (pair.secondAssign.lowerBound <= pair.firstAssign.lowerBound
                && pair.secondAssign.upperBound >= pair.firstAssign.upperBound)
                return true;

            return false;
        }

        AssignmentPair[] LoadAssigmentPairs()
        {
            return this.ReadLines()
                .Select(assignmentPairStr => assignmentPairStr.Split(','))
                .Select(assignmentPair => assignmentPair
                    .Select(assignmentStr => assignmentStr.Split('-').Select(int.Parse).ToArray())
                    .Select(assignmentBounds => new Assignment(assignmentBounds[0], assignmentBounds[1]))
                    .ToArray()
                )
                .Select(assignmentBoundsPair => new AssignmentPair(assignmentBoundsPair[0], assignmentBoundsPair[1]))
                .ToArray();
        }
        public override void SolvePart1()
        {
            int numContained = this.LoadAssigmentPairs()
                .Where(this.isFullyInPair)
                .Count();
            Console.WriteLine($"Pairs fully contained in another: {numContained}");
        }

        public override void SolvePart2()
        {
            int numOverlapped = this.LoadAssigmentPairs()
                .Where(this.hasOverlap)
                .Count();
            Console.WriteLine($"Pairs with overlap: {numOverlapped}");
        }
    }
}
