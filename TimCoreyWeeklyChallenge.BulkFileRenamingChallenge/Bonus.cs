using System;
using System.IO;
using System.Linq;
using CommandLine;

namespace TimCoreyWeeklyChallenge.BulkFileRenamingChallenge
{
    [Verb("bonus", HelpText = "Rename files based on their content")]
    internal class Bonus : ITestableCommand
    {
        public string Source { get; set; }

        public bool IsTesting { get; set; }

        public int Run()
        {
            if (!Directory.Exists(Source))
            {
                Console.WriteLine("Exiting. Source directory not found.");
                return 1;
            }

            Directory.EnumerateFiles(Source)
                .Select(filename => (Original: filename, New: RenameFileWithContentsOfFirstLine(filename)))
                .ToList()
                .ForEach(tuple =>
                {
                    if (IsTesting)
                    {
                        Console.WriteLine($"{tuple}");
                    }
                    else
                    {
                        File.Move(tuple.Original, tuple.New);
                    }
                });

            return 0;
        }
        private static string RenameFileWithContentsOfFirstLine(string fileName)
        {
            return FileNameParts.Parse(fileName).WithNewName(_ => File.ReadLines(fileName).First()).Combine();
        }
    }
}