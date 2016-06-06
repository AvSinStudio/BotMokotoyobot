using System.Linq;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Wrappers
{
    public class TaskWrapper:Wrapper
    {
        public TaskWrapper(PyTaskDatabase db) : base(db)
        {
        }

        public override string GetWrapped(string query)
        {
            var info = db.GetInfoAboutTask(query);
            var stat = info.PointsStat;
            return $"Таск: {query}\n\n" +
                   $"Категория: {info.Category}\n" +
                   $"Максимальный балл: {stat.Max}\n" +
                   $"Средний балл: {stat.AveragePoint} ({stat.AveragePercent}%)\n" +
                   $"Сдавшие: {info.TakenTasks.Count}\n" +
                   $"На полный балл {stat.FullPointsAmount} ({stat.FullPointsPercent}%)";
        }
    }
}