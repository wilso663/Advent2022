using System.Collections.Generic;

namespace AdventOfCode2022.Days.Day5.DTOs
{
   internal readonly record struct Day5Input(Stack<Crate>[] Stacks, MovementOperation[] movementOperations);
    
}
