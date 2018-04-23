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

            // Create two ITrackable variables to store Taco Bell locations.
            ITrackable locA = null;
            ITrackable locB = null;

            // Create a variable to store the distance between two Taco Bell locations.
            double distance = 0.00;

            // For each location in locations: 
            foreach (var loc1 in locations)
            {
                // Grab each location as the origin; store the latitude and longitude in coordinate variable.
                var corA = new GeoCoordinate
                {
                    Longitude = loc1.Location.Longitude,
                    Latitude = loc1.Location.Latitude
                };
                // Within this loop:
                foreach (var loc2 in locations)
                {
                    // Grab another location as the destination; store the latitude and longitude in coordinate variable.
                    var corB = new GeoCoordinate
                    {
                        Longitude = loc2.Location.Longitude,
                        Latitude = loc2.Location.Latitude
                    };
                    
                    // Make a new variable to store the distance between the origin and the destination point.
                    var dist = corA.GetDistanceTo(corB);

                    /* If the distance between the origin and destination is greater than the var distance outside the
                     scope, adjust the var distance. Also adjust the coordinate variables outside of the scope. */
                    if (dist > distance)
                    {
                        locA = loc1;
                        locB = loc2;
                        distance = dist;
                    }
                }
            }

            // Print out the two Taco Bells farthest apart from one another in the .csv file, and the distance in meters.
            Console.WriteLine($"The largest distance in the data is between {locA.Name} and {locB.Name}, at {distance} meters.");
            Console.ReadLine();
        }
    }
}