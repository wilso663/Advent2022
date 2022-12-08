using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day2
{
    internal class GameLogicP1 : GameLogic
    {
        private readonly record struct StrategyGuideEntry(Choice TheirChoice, Choice YourChoice);
        internal override int GetTotalScore(IEnumerable<string> lines)
        {
            return this.GetStrategyGuideEntries(lines)
                .Select(this.GetScore)
                .Sum();
        }
        protected Result GetResult(Choice theirChoice, Choice yourChoice)
        {
            if( theirChoice == yourChoice) { return Result.Draw; }

            else if( theirChoice == Choice.Rock)
            {
                return yourChoice == Choice.Paper ? Result.Win : Result.Lose;
            }

            else if( theirChoice == Choice.Paper)
            {
                return yourChoice == Choice.Scissors ? Result.Win : Result.Lose;
            }

            else if ( theirChoice == Choice.Scissors)
            {
                return yourChoice == Choice.Rock ? Result.Win : Result.Lose;
            }
            return Result.Draw;
        }
        IEnumerable<StrategyGuideEntry> GetStrategyGuideEntries(IEnumerable<string> lines)
        {
            var charChoiceMappings = new Dictionary<char, Choice>
            {
                { 'A', Choice.Rock },
                { 'B', Choice.Paper },
                { 'C', Choice.Scissors },
                { 'X', Choice.Rock },
                { 'Y', Choice.Paper },
                { 'Z', Choice.Scissors },
            };

            return lines.Select(line => new StrategyGuideEntry
            {
                TheirChoice = charChoiceMappings[line[0]],
                YourChoice = charChoiceMappings[line[2]]
            });
        }

        int GetScore(StrategyGuideEntry entry)
        {
            Result result = this.GetResult(entry.TheirChoice, entry.YourChoice);
            return this.GetScore(entry.YourChoice, result);
        }
    }
}
