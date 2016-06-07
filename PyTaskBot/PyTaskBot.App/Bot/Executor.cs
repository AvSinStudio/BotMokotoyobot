using System.Collections.Generic;
using System.Linq;
using PyTaskBot.App.Bot.Commands;

namespace PyTaskBot.App.Bot
{
    internal class Executor
    {
        private readonly List<Command> commands = new List<Command>();
        
        public void Register(Command cmd)
        {
            commands.Add(cmd);
        }

        public string[] GetAvailableCommandsName()
        {
            return commands.Select(x => x.Name).ToArray();
        }

        private Command TryGetCommand(string query)
        {
            var trimmed = query.Trim('/');
            return commands.FirstOrDefault(x => x.CheckAliases(trimmed));
        }

        public string GetResponse(string[] args)
        {
            var cmd = TryGetCommand(args[0]);
            if (cmd == null)
            {
                return "Команда не опознана!";
            }
            return cmd.CreateResponse(args);
        }
    }
}