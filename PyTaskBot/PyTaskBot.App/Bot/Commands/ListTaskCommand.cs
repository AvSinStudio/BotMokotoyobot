using System;
using System.Linq;
using System.Threading.Tasks;
using PyTaskBot.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public class ListTaskCommand: Command
    {
        private PyTaskDatabase db;
        public ListTaskCommand(PyTaskDatabase db) : base("list", "show list of tasks")
        {
            this.db = db;
        }

        public override string CreateResponse(string query)
        {
            return String.Join("\n", db.GetTasks());
        }

        
    }
}