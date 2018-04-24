using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog Logger = new TacoLogger();

        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            Logger.LogInfo("Log initialized");

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

            var parser = new TacoParser();

            // IEnumerable of locations.
            var locations = lines.Select(line => parser.Parse(line));

            // Two Taco Bell locations.
            ITrackable locA = null;
            ITrackable locB = null;
            double distance = 0.00;

            foreach (var loc1 in locations)
            {
                // Store lat and long in coordinate variables for origin and destination Taco Bells.
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
                    
                    // Distance between origin and destination point.
                    var dist = corA.GetDistanceTo(corB);

                    if (dist > distance)
                    {
                        locA = loc1;
                        locB = loc2;
                        distance = dist;
                    }
                }
            }
            Console.WriteLine($"The largest distance in the data is between\n\t{locA.Name}\n\t{locB.Name}\nat {Math.Round(distance * 0.000621371)} miles.");
            Console.ReadLine();
        }
    }
}