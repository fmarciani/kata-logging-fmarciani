using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        static readonly ILog Logger = new TacoLogger();

        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            Logger.LogInfo("Log initialized");
            //var csvPath = Environment.CurrentDirectory + "\\TacoBell-US-AL.csv";

            Logger.LogInfo("Reading lines");
            var lines = File.ReadAllLines(csvPath);

            Logger.LogInfo($"Lines: {lines[0]}");

            switch (lines.Length)
            {
                case 0:
                    Logger.LogError("No lines to read from file.");
                    break;
                case 1:
                    Logger.LogWarning("Only 1 line exists in the file. 2 lines are needed to calculate distance.");
                    break;
                default:
                    break;
            }

            // Create new instance of the TacoParser class.
            var parser = new TacoParser();

            // Grab IEnumerable of locations.
            var locations = lines.Select(line => parser.Parse(line)).ToArray();
            //var locations = lines.Select(line => parser.Parse(line));

            // Create two ITrackable variables to store Taco Bell locations.
            ITrackable locA = null;
            ITrackable locB = null;

            // Create a variable to store the distance between two Taco Bell locations.
            double distance = 0.00;

            // TODO:  Find the two TacoBells in Alabama that are the furthurest from one another.
            // HINT:  You'll need two nested forloops

            foreach (var loc1 in locations)
            {
                var corA = new GeoCoordinate
                {
                    Longitude = loc1.Location.Longitude,
                    Latitude = loc1.Location.Latitude
                };
                foreach (var loc2 in locations)
                {
                    var corB = new GeoCoordinate
                    {
                        Longitude = loc2.Location.Longitude,
                        Latitude = loc2.Location.Latitude
                    };
                    var dist = corA.GetDistanceTo(corB);
                    if (dist > distance)
                    {
                        locA = loc1;
                        locB = loc2;
                        distance = dist;
                    }
                }
            }

            Console.WriteLine($"The largest distance in the data is between {locA.Name} and {locB.Name}, at {distance} meters.");
            Console.ReadLine();
        }
    }
}