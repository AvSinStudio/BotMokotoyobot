using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace PyTaskBot.Domain
{
    public class Task
    {

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("students")]
        public List<TakenTask> TakenTasks { get; set; }

        // public Dictionary<StudyYear, PointsStat> Annual { get; set; }
        [JsonProperty("max")]
        private int Max { get; set; }

        [JsonProperty("average_points")]
        private double AveragePoint { get; set; }

        [JsonProperty("average_percent")]
        private double AveragePercent { get; set; }

        [JsonProperty("full_points_percent")]

        private double FullPointsPercent { get; set; }

        [JsonProperty("students_full_points")]
        private int FullPointsAmount { get; set; }

        public PointsStat PointsStat => new PointsStat
        {
            AveragePercent = AveragePercent,
            AveragePoint = AveragePoint,
            Max = Max,
            FullPointsPercent = FullPointsPercent,
            FullPointsAmount = FullPointsAmount
        };
    }
}