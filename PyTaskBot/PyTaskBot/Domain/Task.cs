using System.Collections.Generic;

namespace DataBase.Model
{
    public class Task
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<TakenTask> TakenTasks { get; set; }
        public PointsStat PointsStat { get; set; }
        public Dictionary<StudyYear, PointsStat> Annual { get; set; }


    }
}