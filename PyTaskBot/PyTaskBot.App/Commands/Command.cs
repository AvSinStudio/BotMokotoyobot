using System;
using System.Collections.Generic;
using System.Linq;

namespace PyTaskBot.App.Commands
{
    public abstract class Command
    {
        protected readonly HashSet<string> Aliases;

        protected Command(string name, string help)
        {
            Name = name;
            Help = help;
            Aliases = new HashSet<string> {Name};
        }

        public string Name { get; }
        public string Help { get; }

        public virtual bool HasAlias(string alias)
        {
            return Aliases.Contains(alias, StringComparer.OrdinalIgnoreCase);
        }

        public abstract string CreateResponse(params object[] args);
    }
}