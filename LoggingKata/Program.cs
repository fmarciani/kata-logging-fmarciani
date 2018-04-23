using System;
using System.Linq;
using System.IO;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        static readonly ILog Logger = new TacoLogger();

        //const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            Logger.LogInfo("Log initialized");
            var csvPath = Environment.CurrentDirectory + "\\TacoBell-US-AL.csv";

            Logger.LogInfo("Reading lines");
            var lines = File.ReadAllLines(csvPath);

            Logger.LogInfo($"Lines: {lines[0]}");

            switch (lines.Length)
            {
                case 0:
                    Logger.LogError("0 lines read in input file.");
                    break;
                case 1:
                    Logger.LogWarning("Only 1 line read in input file.");
                    break;
                default:
                    break;
            }

            // Create new instance of the TacoParser class.
            var parser = new TacoParser();

            // Grab IEnumerable of locations.
            var locations = lines.Select(line => parser.Parse(line));

            // TODO:  Find the two TacoBells in Alabama that are the furthurest from one another.
            // HINT:  You'll need two nested forloops
        }
    }
}