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
            return commands.FirstOrDefault(x => x.HasAlias(trimmed));
        }

        public string GetResponse(params object[] args)
        {
            var cmd = TryGetCommand(args[0] as string);
            return cmd == null ? "Команда не опознана!" : cmd.CreateResponse(args);
        }
    }
}