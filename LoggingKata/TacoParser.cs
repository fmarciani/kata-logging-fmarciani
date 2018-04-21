namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the TacoBells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Read line, split it by comma into string array.
            var cells = line.Split(',');

            // If the length of the array != 3, return null.
            if (cells.Length != 3)
            {
                return null;
            }
            //DO not fail if one record parsing fails, return null
            return null; //TODO Implement
        }
    }
}