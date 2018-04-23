using System;
using System.IO;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the TacoBells
    /// </summary>
    public class TacoParser
    {
        public readonly ILog Logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            Logger.LogInfo("Begin parsing");
            
            // Read line, split it by comma into string array.
            var cells = line.Split(',');

            // If the line is empty or the length of the array != 3, return null.
            if (string.IsNullOrEmpty(line))
            {
                Logger.LogError("Empty line detected.");
                return null;
            }
            else if (cells.Length != 3)
            {
                Logger.LogError("Line entry has fewer than 3 cells.");
                return null;
            }

            // Location name should be in cell index 2. Parse lat and lon as doubles.
            Logger.LogInfo("Parsing location");

            var name = cells[2];

            /*
             // Remove all characters in location cell beginning with char "(".
             // UNSURE WHY THIS DOESN'T WORK; FAILS MULTIPLE TESTS: 
             var uneditedName = cells[2];
             var name = uneditedName.Remove(uneditedName.IndexOf('('));
            */

            double lon;
            double lat;

            try
            {
                Logger.LogInfo("Parsing longitude");
                lon = double.Parse(cells[0]);

                Logger.LogInfo("Parsing latitude");
                lat = double.Parse(cells[1]);

                if (Math.Abs(lon) > 180 || Math.Abs(lat) > 90)
                {
                    Logger.LogError("Longitude or latitude out of range.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Could not parse coordinates.", e);
                return null;
            }

            var tacoBell = new TacoBell { Location = new Point { Longitude = lon, Latitude = lat }, Name = name };
            return tacoBell;
        }
    }
}