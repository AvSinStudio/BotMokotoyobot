using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public class HelpCommand:Command
    {
        public HelpCommand() : base("help", "print help")
        {
        }

        public override void Execute(string query, long id, Func<long, string, Task<Message>> sender)
        {
            sender(id, "help");
        }
        
    }
}