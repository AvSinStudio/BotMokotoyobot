using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Commands
{
    public class ListTaskCommand : Command
    {
        private readonly PyTaskDatabase db;

        public ListTaskCommand(PyTaskDatabase db) : base("list", "show list of tasks")
        {
            this.db = db;
        }

        public override string CreateResponse(params object[] args)
        {
            return string.Join("\n", db.SortedTasksNames);
        }
    }
}