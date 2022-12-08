using AdventOfCode2022.Days.Day5.DTOs;
using AdventOfCode2022.Days.Day5.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day5
{
    internal class Day5: Day
    {
        public override void SolvePart1()
        {
            this.RunCraneSimulation(new CraneLogicP1());
        }

        public override void SolvePart2() 
        {
            this.RunCraneSimulation(new CraneLogicP2());
        }

        IEnumerable<Crate> GetCrateAtTopOfEachStack(Stack<Crate>[] stacks)
        {
            foreach(Stack<Crate> stack in stacks)
            {
                if(stack.TryPop(out Crate topCrate))
                {
                    yield return topCrate;
                }
            }
        }
        void RunCraneSimulation(CraneLogic craneLogic)
        {
            var inputParser = new Day5InputParser();
            (Stack<Crate>[] stacks, MovementOperation[] movementOperations) = inputParser.Parse(this.ReadLines());

            foreach(MovementOperation movementOperation in movementOperations) 
            {
                craneLogic.PerformMovementOperation(ref stacks, movementOperation);
            }

            foreach (Crate topCrate in this.GetCrateAtTopOfEachStack(stacks))
                Console.WriteLine($"Top crate ID: {topCrate.ID}");
        }
    }
}
