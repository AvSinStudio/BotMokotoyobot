using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PyTaskBot.App.Bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot
{
    class Executor
    {
        private readonly List<Command> commands = new List<Command>();
        private MessageParser mp;
        private Func<long, string, Task<Message>> sender; 
        public Executor(Func<long, string, Task<Message>> sender )
        {
            this.sender = sender;
        }

        public void Register(Command cmd)
        {
            commands.Add(cmd);
            mp = new MessageParser();
        }

        public string[] GetAvailableCommandsName()
        {
            return commands.Select(x => x.Name).ToArray();
        }

        public Command FindCommandByName(string query)
        {
            var words = query.Split(' ');
            return commands.FirstOrDefault(x => string.Equals('/' + x.Name, words[0], StringComparison.OrdinalIgnoreCase));
        }

        public void Execute(string query, long id)
        {
            var cmd = FindCommandByName(query);
            if (cmd == null)
            {
                var i = "";
                mp.TryParse(query, out i);
            }
            else
                cmd.Execute(query, id, sender);
        }
    }
}
