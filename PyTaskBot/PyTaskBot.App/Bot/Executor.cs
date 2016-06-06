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
        
        
        public Executor()
        {
        }

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

        public string GetResponse(string query)
        {
            var cmd = TryGetCommand(query);
            if (cmd == null)
            {
                return "Not recognized";

            }
            else
               return cmd.CreateResponse(query);
        }
    }
}
