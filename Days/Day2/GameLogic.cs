using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2022.Days.Day2
{
    abstract class GameLogic
    {
        internal abstract int GetTotalScore(IEnumerable<string> lines);

        protected int GetScore(Choice yourChoice, Result result)
        {
            var choiceScores = new Dictionary<Choice, int> {
                { Choice.Rock, 1},
                { Choice.Paper, 2 },
                { Choice.Scissors, 3 }
            };
            var resultScores = new Dictionary<Result, int>
            {
                { Result.Lose, 0 },
                { Result.Draw, 3 },
                { Result.Win, 6 },
            };
            return choiceScores[yourChoice] + resultScores[result];
        }

    }
}
