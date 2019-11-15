using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;

namespace TimCoreyWeeklyChallenge.BulkFileRenamingChallenge
{
    [Verb("challenge", HelpText = "Rename files and change company name")]
    internal class Challenge : ITestableCommand
    {
        public string Source { get; set; }

        [Option("old-company-name", Default = "Acme")]
        public string OldCompanyName { get; set; }

        [Option("new-company-name", Default = "TimCo")]
        public string NewCompanyName { get; set; }

        public bool IsTesting { get; set; }


        public int Run()
        {
            if (!Directory.Exists(Source))
            {
                Console.WriteLine("Exiting. Source directory not found.");
                return 1;
            }

            Directory.EnumerateFiles(Source)
                .Select(f => (Original: f, New: RenameFileWithTitleCaseAndCompanyName(f, OldCompanyName, NewCompanyName)))
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

        private static string RenameFileWithTitleCaseAndCompanyName(string fileName, string oldCompanyName, string newCompanyName)
        {
            return FileNameParts.Parse(fileName).WithNewName(name => Rename(name, oldCompanyName, newCompanyName)).Combine();
        }

        private static string Rename(string name, string oldCompanyName, string newCompanyName)
        {
            string ReplaceCompanyName(string word) => ReplaceWord(word, oldCompanyName, newCompanyName);

            return string.Join(' ', name.ToLower()
                .Split(' ')
                .Select(TitleCase)
                .Select(ReplaceCompanyName));
        }

        private static string TitleCase(string word)
        {
            return new string(word.Select((letter, index) => index == 0 ? char.ToUpper(letter) : letter).ToArray());
        }

        private static string ReplaceWord(string word, string wordToFind, string wordToReplaceItWith)
        {
            return string.Equals(word, wordToFind, StringComparison.CurrentCultureIgnoreCase)
                ? wordToReplaceItWith
                : word;
        }
    }
}