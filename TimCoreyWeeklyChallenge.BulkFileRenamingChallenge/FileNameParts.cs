using System;
using System.IO;

namespace TimCoreyWeeklyChallenge.BulkFileRenamingChallenge
{
    public struct FileNameParts
    {
        public string Directory { get; }
        public string Name { get; }
        public string Extension { get; }


        public FileNameParts(string filename)
        {
            Directory = Path.GetDirectoryName(filename);
            Name = Path.GetFileNameWithoutExtension(filename);
            Extension = Path.GetExtension(filename);
        }

        public static FileNameParts Parse(string filename)
        {
            return new FileNameParts(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename));
        }

        private FileNameParts(string directory, string name, string extension)
        {
            Directory = directory;
            Name = name;
            Extension = extension;
        }

        public string Combine()
        {
            return Path.Combine(Directory, Path.ChangeExtension(Name, Extension));
        }

        public FileNameParts WithNewName(string newNameWithoutExtension)
        {
            return new FileNameParts(Directory, newNameWithoutExtension, Extension);
        }

        public FileNameParts WithNewName(Func<string, string> rename)
        {
            return new FileNameParts(Directory, rename(Name), Extension);
        }
    }
}