using PyTaskBot.App.Bot.Wrappers;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class TaskInfoCommand : Command
    {
        private readonly TaskWrapper taskWrapper;
        private readonly PyTaskDatabase db;
        public TaskInfoCommand(PyTaskDatabase db) : base("info", "give info about the task")
        {
            taskWrapper = new TaskWrapper();
            this.db = db;
            foreach (var x in db.GetTasks())
            {
                Aliases.Add(x);
            }
        }

        public override string CreateResponse(string name)
        {
            var task = db.GetInfoAboutTask(name);
            return taskWrapper.GetWrapped(task);
        }
    }
}