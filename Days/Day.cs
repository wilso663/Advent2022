using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    abstract class Day : IDay
    {
        public void Solve()
        {
            int i = 1;
            uint dayNumber = this.DayNumber;
            foreach (Action partSolver in this.PartSolvers)
            {
                Console.WriteLine($"Day {this.DayNumber} Part {i}");
                partSolver.Invoke();
                Console.WriteLine("");
                ++i;
            }
            Utils.WaitForKeyPress();
        }

        public abstract void SolvePart1();
        public abstract void SolvePart2();

        protected virtual Action[] PartSolvers => new Action[]
        {
            this.SolvePart1,
            this.SolvePart2,
        };

        protected uint DayNumber => uint.Parse((Regex.Match(this.GetType().Name, @"(\d+)").Value));

        protected string[] ReadLines()
        {
            //Big hard code path, Assembly location is in another path.
            string root = "C:\\Users\\ARMSTRONG\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\";
            
            string day = this.GetType().Name;
            string path = Path.Combine(root, @$"Days\{day}\{day}Input.txt");
            return File.ReadAllLines(path);
        }

        protected string ReadIntoLine()
        {
            string root = "C:\\Users\\ARMSTRONG\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\";

            string day = this.GetType().Name;
            string path = Path.Combine(root, @$"Days\{day}\{day}Input.txt");
            return File.ReadAllText(path);
        }
    }
}
