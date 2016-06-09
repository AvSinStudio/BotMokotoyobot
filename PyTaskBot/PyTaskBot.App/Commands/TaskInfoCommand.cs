using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Commands
{
    public class TaskInfoCommand : Command
    {
        private readonly PyTaskDatabase db;

        public TaskInfoCommand(PyTaskDatabase db) : base(new [] { "info" }, "give info about the task")
        {
            this.db = db;
            foreach (var x in db.TasksNamesSet)
            {
                Names.Add(x);
            }
        }

        public override string CreateResponse(object[] args)
        {
            var name = args[0] as string;

            var task = db.GetTask(name);
            return task.ToString();
        }
    }
}