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

            if (string.IsNullOrEmpty(line))
            {
                Logger.LogError("Empty line detected.");
                return null;
            }

            var cells = line.Split(',');

            if (cells.Length < 2)
            {
                Logger.LogError("Input failed to parse.");
                return null;
            }

            Logger.LogInfo("Parsing location");

            var name = cells[2];

            try
            {
                Logger.LogInfo("Parsing longitude");
                var lon = double.Parse(cells[0]);

                Logger.LogInfo("Parsing latitude");
                var lat = double.Parse(cells[1]);

                if (Math.Abs(lon) > 180 || Math.Abs(lat) > 90)
                {
                    Logger.LogError("Longitude or latitude out of range.");
                    return null;
                }

                var point = new Point {Longitude = lon, Latitude = lat};
                return new TacoBell { Location = point, Name = name };
            }
            catch (Exception e)
            {
                Logger.LogError("Could not parse coordinates.", e);
                return null;
            }
        }
    }
}