using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Wrappers
{
    public class CatListWrapper : Wrapper
    {
        public CatListWrapper(PyTaskDatabase db) : base(db)
        {
        }

        public override string GetWrapped(string query)
        {
            var tasks = db.GetNamesOfTasksInCategory(query);
            return $"Категория: {query}\n\n" +
                   $"Таски:\n" +
                   string.Join("\n", tasks);
        }
    }
}