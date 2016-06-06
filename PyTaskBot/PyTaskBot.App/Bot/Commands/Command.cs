using System;
using System.Collections.Generic;
using System.Linq;
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
            this.Aliases = new List<string>();
            Aliases.Add(Name);
            this.Help = help;
        }
        public string Name { get; }
        public string Help { get; }
        protected List<String> Aliases;

        public bool CheckAliases(string query)
        {
            return Aliases.Any(x => string.Equals(x, query, StringComparison.OrdinalIgnoreCase));
        }

        protected Database Db;
        public abstract string CreateResponse(string query);
    }
}