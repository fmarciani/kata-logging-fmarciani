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

            // If the length of the array != 3, return null.
            if (cells.Length != 3)
            {
                Logger.LogWarning("Line entry with fewer than three cells found.");
                return null;
            }

            // Location name should be in cell index 2. Parse lat and lon as doubles.
            var name = cells[2];
            double lon;
            double lat;

            try
            {
                lon = Convert.ToDouble(cells[0]);
                lat = Convert.ToDouble(cells[1]);
            }
            catch (Exception)
            {
                Logger.LogWarning("Could not parse coordinates.");
                return null;
            }

            if (Math.Abs(lon) <= 180 && Math.Abs(lat) <= 90)
            {
                return cells;
            }
            else
            {
                return null;
            }

/*
            var tacoBell = new TacoBell();

            return tacoBell;
*/
        }
    }
}