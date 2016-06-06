using System;
using System.Threading.Tasks;
using PyTaskBot.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public abstract class Command
    {
        protected Command(string name, string help)
        {
            this.Name = name;
            this.Help = help;
        }
        public string Name { get; }
        public string Help { get; }
        protected Database Db;
        public abstract void Execute(string query, long id, Func<long, string, Task<Message>> sender);
    }
}