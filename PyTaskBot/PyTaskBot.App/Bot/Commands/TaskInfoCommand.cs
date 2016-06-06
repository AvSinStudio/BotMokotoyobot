using PyTaskBot.App.Bot.Wrappers;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class TaskInfoCommand : Command
    {
        private readonly TaskWrapper taskWrapper;

        public TaskInfoCommand(PyTaskDatabase db) : base("info", "give info about the task")
        {
            taskWrapper = new TaskWrapper();

            foreach (var x in db.GetTasks())
            {
                Aliases.Add(x);
            }
        }

        public override string CreateResponse(string name)
        {
            var task = Db.GetInfoAboutTask(name);
            return taskWrapper.GetWrapped(task);
        }
    }
}