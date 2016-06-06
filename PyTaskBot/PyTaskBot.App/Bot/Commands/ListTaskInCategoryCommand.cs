using System;
using System.Threading.Tasks;
using PyTaskBot.Infrastructure;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public class ListTaskInCategoryCommand:Command
    {
        private PyTaskDatabase db;

        public ListTaskInCategoryCommand(PyTaskDatabase db) : base("catinfo", "show list of tasks in category")
        {
            this.db = db;
        }

        public override void Execute(string query, long id, Func<long, string, Task<Message>> sender)
        {
            sender(id, "");
        }
    }
}