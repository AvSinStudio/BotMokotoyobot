using PyTaskBot.App.Bot.Wrappers;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class ListTaskInCategoryCommand : Command
    {
        private PyTaskDatabase db;


        public ListTaskInCategoryCommand(PyTaskDatabase db) : base("catinfo", "show list of tasks in category")
        {
            this.db = db;
            foreach (var x in db.GetCategories())
            {
                Aliases.Add(x);
            }
        }

        public override string CreateResponse(string query)
        {
            var tasks = db.GetNamesOfTasksInCategory(query);
            return $"Категория: {query}\n\n" +
                   $"Таски:\n" +
                   string.Join("\n", tasks);
        }
    }
}