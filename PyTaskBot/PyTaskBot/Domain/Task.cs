using System.Collections.Generic;
using Newtonsoft.Json;

namespace PyTaskBot.Domain
{
    public class Task
    {
        [JsonProperty("max")]
        private int Max { get; set; }

        [JsonProperty("average_points")]
        private double AveragePoint { get; set; }

        [JsonProperty("average_percent")]
        private double AveragePercent { get; set; }

        [JsonProperty("full_points_percent")]
        private double FullPointsPercent { get; set; }

        [JsonProperty("students_full_points")]
        private int FullPointsCount { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("students")]
        public List<TakenTask> TakenTasks { get; set; }

        public PointsStat PointsStat => new PointsStat {
            AveragePercent = AveragePercent,
            AveragePoint = AveragePoint,
            Max = Max,
            FullPointsPercent = FullPointsPercent,
            FullPointsCount = FullPointsCount,
            Count = TakenTasks.Count
        };

        public override string ToString()
        {
            return $"Таск: {Name}\n\n" +
                   $"Категория: {Category}\n" +
                   $"Максимальный балл: {Max}\n" +
                   $"Средний балл: {AveragePoint} ({AveragePercent}%)\n" +
                   $"Сдавшие: {TakenTasks.Count}\n" +
                   $"На полный балл {FullPointsCount} ({FullPointsPercent}%)";
        }
    }
}