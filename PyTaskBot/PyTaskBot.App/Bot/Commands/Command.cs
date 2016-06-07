using System;
using System.Collections.Generic;
using System.Linq;

namespace PyTaskBot.App.Bot.Commands
{
    public abstract class Command
    {
        protected List<string> Aliases;
        protected Command(string name, string help)
        {
            Name = name;
            Aliases = new List<string> {Name};
            Help = help;
        }

        public string Name { get; }
        public string Help { get; }

        public virtual bool CheckAliases(string query)
        {
            return Aliases.Any(x => string.Equals(x, query, StringComparison.OrdinalIgnoreCase));
        }

        public abstract string CreateResponse(string[] args);
    }
}