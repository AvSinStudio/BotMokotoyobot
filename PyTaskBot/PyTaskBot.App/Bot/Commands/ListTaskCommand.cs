using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class ListTaskCommand : Command
    {
        private readonly PyTaskDatabase db;

        public ListTaskCommand(PyTaskDatabase db) : base("list", "show list of tasks")
        {
            this.db = db;
        }

        public override string CreateResponse(string[] args)
        {
            return string.Join("\n", db.GetTasks());
        }
    }
}