namespace PyTaskBot.Domain
{
    public class PointsStat
    {
        public int Max { get; set; }
        public double AveragePoint { get; set; }
        public double AveragePercent { get; set; }
        public double FullPointsPercent { get; set; }
        public int FullPointsCount { get; set; }
        public int Count { get; set; }
    }
}