using System;
using System.Collections.Generic;
using System.Linq;

namespace PyTaskBot.App.Commands
{
    public abstract class Command
    {
        public readonly HashSet<string> Names;

        public string Help { get; }

        protected Command(IEnumerable<string> names, string help)
        {
            Names = new HashSet<string>(names);
            Help = help;
        }
        
        public virtual bool CanBeCalledBy(string name)
        {
            return Names.Contains(name, StringComparer.OrdinalIgnoreCase);
        }

        public abstract string CreateResponse(object[] args);
    }
}