using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Commands
{
    public class TaskInfoCommand : Command
    {
        private readonly PyTaskDatabase db;
        private readonly TaskWrapper taskWrapper;

        public TaskInfoCommand(PyTaskDatabase db) : base("info", "give info about the task")
        {
            taskWrapper = new TaskWrapper();
            this.db = db;
            foreach (var x in db.TasksNamesSet)
            {
                Aliases.Add(x);
            }
        }

        public override string CreateResponse(params object[] args)
        {
            var name = args[0] as string;

            var task = db.GetTask(name);
            return taskWrapper.GetWrapped(task);
        }
    }
}