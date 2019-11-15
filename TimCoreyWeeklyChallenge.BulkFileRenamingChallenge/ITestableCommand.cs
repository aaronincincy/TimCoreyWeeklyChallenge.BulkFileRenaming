using CommandLine;

namespace TimCoreyWeeklyChallenge.BulkFileRenamingChallenge
{
    public interface ITestableCommand
    {
        [Option('t', "testing")]
        bool IsTesting { get; set; }

        [Value(0, MetaName = "source", HelpText = "Source directory", Required = true)]
        string Source { get; set; }
    }
}