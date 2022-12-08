using System.Collections.Generic;
using AdventOfCode2022.Days.Day5.DTOs;


namespace AdventOfCode2022.Days.Day5
{
    class CraneLogicP1: CraneLogic
    {
        internal override void PerformMovementOperation(ref Stack<Crate>[] stacks, MovementOperation movementOperation)
        {
            for(int i = 0; i < movementOperation.moveAmount; ++i)
            {
                this.MoveCrate(stacks[movementOperation.fromStack - 1], stacks[movementOperation.toStack - 1]);
            }
        }
    }
}
