using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day11
{
    internal class Day11 : Day
    {
        public override void SolvePart1()
        {
            var input = this.ReadIntoLine();
            var monkeys = ParseMonkeys(input);
            RunRounds(20, monkeys, w => w / 3);
            long businessLevel = GetMonkeyBusinessLevel(monkeys);
            Console.WriteLine($"Current monkey business level: {businessLevel}");
        }

        public override void SolvePart2()
        {
            var input = this.ReadIntoLine();
            var monkeys = ParseMonkeys(input);
            var mod = monkeys.Aggregate(1, (mod, monkey) => mod * monkey.modulus);
            RunRounds(10_000, monkeys, w => w % mod);
            long businessLevel = GetMonkeyBusinessLevel(monkeys);
            Console.WriteLine($"P2 monkey business: {businessLevel}");
        }

        Monkey ParseMonkey(string input)
        {
            var monkey = new Monkey();
            foreach (var line in input.Split("\r\n"))
            {
                var tryParse = LineParser(line);
                if (tryParse(@"Monkey (\d+)", out var arg))
                {
                    // pass
                }
                else if (tryParse("Starting items: (.*)", out arg))
                {
                    monkey.items = new Queue<long>(arg.Split(", ").Select(long.Parse));
                }
                else if (tryParse(@"Operation: new = old \* old", out _))
                {
                    monkey.operation = old => old * old;
                }
                else if (tryParse(@"Operation: new = old \* (\d+)", out arg))
                {
                    monkey.operation = old => old * int.Parse(arg);
                }
                else if (tryParse(@"Operation: new = old \+ (\d+)", out arg))
                {
                    monkey.operation = old => old + int.Parse(arg);
                }
                else if (tryParse(@"Test: divisible by (\d+)", out arg))
                {
                    monkey.modulus = int.Parse(arg);
                }
                else if (tryParse(@"If true: throw to monkey (\d+)", out arg))
                {
                    monkey.passToMonkeyIfDividesBy = int.Parse(arg);
                }
                else if (tryParse(@"If false: throw to monkey (\d+)", out arg))
                {
                    monkey.passToMonkeyOtherwise = int.Parse(arg);
                }
                else
                {
                    throw new ArgumentException(line);
                }
            }
            return monkey;
        }
        Monkey[] ParseMonkeys(string input)
        {
            return input.Split("\r\n\r\n").Select(ParseMonkey).ToArray();
        }

        private long GetMonkeyBusinessLevel(IEnumerable<Monkey> monkeys)
        {
            return monkeys.OrderByDescending(monkey => monkey.inspectedItems).Take(2).Aggregate(1L, (res,monkey) => res * monkey.inspectedItems);
        }

        private void RunRounds(int rounds, Monkey[] monkeys, Func<long,long> updateWorryLevel)
        {
            for(var i = 0; i < rounds; i++)
            {
                foreach(var monkey in monkeys)
                {
                    while (monkey.items.Any())
                    {
                        monkey.inspectedItems++;

                        var item = monkey.items.Dequeue();
                        item = monkey.operation(item);
                        item = updateWorryLevel(item);

                        var targetMonkey = item % monkey.modulus == 0 ? monkey.passToMonkeyIfDividesBy : monkey.passToMonkeyOtherwise;

                        monkeys[targetMonkey].items.Enqueue(item);
                    }
                }
            }
        }

        TryParse LineParser(string line)
        {
            bool match(string pattern, out string arg)
            {
                var match = Regex.Match(line,pattern);
                if (match.Success)
                {
                    arg = match.Groups[match.Groups.Count - 1].Value;
                    return true;
                } else
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
