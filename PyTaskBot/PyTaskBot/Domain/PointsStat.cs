using Newtonsoft.Json;

namespace PyTaskBot.Domain
{
    public class PointsStat
    {
        [JsonProperty("max")]
        public int Max { get; set; }
        [JsonProperty("average_points")]
        public double AveragePoint { get; set; }
        [JsonProperty("average_percent")]
        public double AveragePercent { get; set; }
        [JsonProperty("full_points_percent")]
        public double FullPointsPercent { get; set; }
    }
}