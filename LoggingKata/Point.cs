namespace LoggingKata
{
    public struct Point
    {
        public static readonly double MaxLat = 90.0;
        public static readonly double MaxLong = 180.0;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}