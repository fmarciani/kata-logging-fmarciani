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
            Logger.LogInfo("Begin parsing ...");
            
            // Read line, split it by comma into string array.
            var cells = line.Split(',');

            // If the length of the array < 3, return null.
            if (cells.Length <= 3)
            {
                Logger.LogError("Line entry has fewer than 3 cells.");
                return null;
            }

            // Location name should be in cell index 2. Parse lat and lon as doubles.
            var name = cells[2];
            double lon;
            double lat;

            try
            {
                Logger.LogInfo("Parsing longitude ...");
                lon = double.Parse(cells[0]);

                Logger.LogInfo("Parsing latitude ...");
                lat = double.Parse(cells[1]);
            }
            catch (Exception)
            {
                Logger.LogError("Could not parse coordinates.");
                return null;
            }

            var tacoBell = new TacoBell() { Location = new Point(), Name = name };

            return tacoBell;
        }
    }
}