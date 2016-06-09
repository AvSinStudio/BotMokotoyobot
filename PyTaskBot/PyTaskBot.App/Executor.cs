using System.Collections.Generic;
using System.Linq;
using PyTaskBot.App.Commands;

namespace PyTaskBot.App
{
    internal class Executor
    {
        private readonly List<Command> commands = new List<Command>();

        public void Register(Command cmd)
        {
            commands.Add(cmd);
        }

        public string[] GetAvailableCommands()
        {
            return commands.SelectMany(x => x.Names).ToArray();
        }

        private Command TryGetCommand(string commandName)
        {
            var trimmed = commandName.Trim('/');
            return commands.FirstOrDefault(x => x.CanBeCalledBy(trimmed));
        }

        public string Execute(string commandName, object[] args)
        {
            var cmd = TryGetCommand(commandName);
            return cmd == null ? "Команда не опознана!" : cmd.CreateResponse(args);
        }
    }
}