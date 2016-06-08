using PyTaskBot.Domain;

namespace PyTaskBot.App.Bot.Wrappers
{
    public class TaskWrapper
    {
        public string GetWrapped(Task task)
        {
            var stat = task.PointsStat;
            return $"Таск: {task.Name}\n\n" +
                   $"Категория: {task.Category}\n" +
                   $"Максимальный балл: {stat.Max}\n" +
                   $"Средний балл: {stat.AveragePoint} ({stat.AveragePercent}%)\n" +
                   $"Сдавшие: {task.TakenTasks.Count}\n" +
                   $"На полный балл {stat.FullPointsCount} ({stat.FullPointsPercent}%)";
        }
    }
}