using CommandLine;

namespace TimCoreyWeeklyChallenge.BulkFileRenamingChallenge
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<Challenge, Bonus>(args).MapResult(
                (Challenge challenge) => challenge.Run(),
                (Bonus bonus) => bonus.Run(),
                errors => 1);
        }
    }
}
