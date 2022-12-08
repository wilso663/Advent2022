using AdventOfCode2022.Days.Day5.DTOs;
using System.Text.RegularExpressions;


namespace AdventOfCode2022.Days.Day5.Parsers
{
    class MovementOperationParser
    {
        private readonly static Regex movementOperationLineRegex = new Regex(@"(?:move\s)(\d+)(?:\sfrom\s)(\d+)(?:\sto\s)(\d+)", RegexOptions.Compiled);
        internal MovementOperation ParseMovementOperation(string movementOperationLine)
        {
            Match match = movementOperationLineRegex.Match(movementOperationLine);
            return new MovementOperation()
            {
                moveAmount = int.Parse(match.Groups[1].Value),
                fromStack = int.Parse(match.Groups[2].Value),
                toStack = int.Parse(match.Groups[3].Value),
            };
        }
    }
}
