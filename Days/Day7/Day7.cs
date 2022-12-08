using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day7
{
    internal class Day7: Day 
    {
 
        public override void SolvePart1()
        {
            var input = this.ReadLines().Skip(1).ToList();
            ElfDirectory root = new ElfDirectory { Name = "/" };
            HydrateFileSystem(root, input);

            List<int> directorySizes = new();
            Traverse(root, directorySizes);

            int totalSum = directorySizes.Where(x => x <= 100000).Sum();
            Console.WriteLine($"The sum of directories under 100k size: {totalSum}.");
        }

        public override void SolvePart2()
        {
            var input = this.ReadLines().Skip(1).ToList();
            ElfDirectory root = new ElfDirectory { Name = "/" };
            HydrateFileSystem(root, input);

            List<int> directorySizes = new();
            Traverse(root, directorySizes);

            var neededSpace = 30000000 - (70000000 - root.Size);
            int topDirectorySize = directorySizes.Order().First(x => x >= neededSpace);
            Console.WriteLine($"The largest directory size under the limit: {topDirectorySize}");
        }

        void Traverse(ElfDirectory node, IList<int> sizes)
        {
            if (node == null) return;

            sizes.Add(node.Size);

            foreach(var dir in node.SubDirectories)
                Traverse(dir, sizes);
        }

        void HydrateFileSystem(ElfDirectory root, IList<string> input)
        {
            var currentDirectory = root;

            foreach(var line in input)
            {
                var currentLine = line.Split(' ');
                if (currentLine[0] == "dir")
                {
                    currentDirectory.AddDirectory(new() { Name = currentLine[1], Parent = currentDirectory});
                }

                if (int.TryParse(currentLine[0], out int num))
                {
                    currentDirectory.AddFile(new() {  Name = currentLine[1], Parent = currentDirectory, Size = num});
                    continue;
                }

                if(currentLine is ["$", "cd", ..])
                {
                    if (currentLine[2] == "..")
                    {
                        currentDirectory = currentDirectory.Parent;
                        continue;
                    }

                    currentDirectory = currentDirectory.SubDirectories.Single(x => x.Name == currentLine[2]);
                }
            }
        }
    }
}
