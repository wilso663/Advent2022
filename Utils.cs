using System;

namespace AdventOfCode2022
{
    static class Utils
    {
        //I don't remember what I was doing with this...
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
