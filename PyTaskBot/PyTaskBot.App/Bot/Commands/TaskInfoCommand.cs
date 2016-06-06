using System;
using System.Threading.Tasks;
using PyTaskBot.App.Bot.Wrappers;
using PyTaskBot.Infrastructure;

using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public class TaskInfoCommand: Command
    {
        private TaskWrapper taskWrapper;
        public TaskInfoCommand(PyTaskDatabase db) : base("info", "give info about the task")
        {
            taskWrapper = new TaskWrapper(db);
            
            foreach (var x in db.GetTasks())
            {
                Aliases.Add(x);
            }
        }

        public override string CreateResponse(string query)
        {
            return taskWrapper.GetWrapped(query);
        }
    }
}