using System;
using System.Threading.Tasks;
using PyTaskBot.Infrastructure;

using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public class TaskInfoCommand: Command
    {
        public TaskInfoCommand(Database db) : base("info", "give info about the task")
        {
        }

        public override void Execute(string query, long id, Func<long, string, Task<Message>> sender)
        {
            sender(id, "allo");
        }
    }
}