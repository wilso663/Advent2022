using System;

namespace AdventOfCode2022
{
    static class Utils
    {
        static internal void WaitForKeyPress()
        {
            while(Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }

            Console.ReadKey();
        }
    }
}
