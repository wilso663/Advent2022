using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode2022.Days.Day7
{
    internal record ElfDirectory
    {
        public string Name { get; init; }
        public ElfDirectory Parent { get; init; }
        public IList<ElfDirectory> SubDirectories { get; private set; } = new List<ElfDirectory>();
        public IList<ElfFile> Files { get; private set; } = new List<ElfFile>();
        public int Size => SubDirectories.Sum(sd => sd.Size) + Files.Sum(f => f.Size);

        public void AddDirectory(ElfDirectory dir) => SubDirectories.Add(dir);
        public void AddFile(ElfFile file) => Files.Add(file);
    }
}
