using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PyTaskBot.App.Bot.Commands;
using PyTaskBot.Infrastructure;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot
{
    class TelegramBot
    {
        private readonly Telegram.Bot.Api botApi;
        private int Offset { get; set; }
        private Sender sender;
        private readonly Executor executor;
        public TelegramBot(string token, PyTaskDatabase database)
        {
            sender = new Sender();
            botApi = new Telegram.Bot.Api(token);
            executor = new Executor();
            executor.Register(new HelpCommand());
            executor.Register(new ListTaskCommand(database));
            executor.Register(new ListCategoriesCommand(database));
            executor.Register(new TaskInfoCommand(database));
            executor.Register(new ListTaskInCategoryCommand(database));
            var me = botApi.GetMe();
            Console.WriteLine($"Hello, I'm {me.Result.Username}");
            Offset = botApi.MessageOffset;
            
        }

        public void MainLoop()
        {
            while (true)
            {
                var updates = botApi.GetUpdates(Offset);
                
                foreach (var update in updates.Result)
                {
                    if (update.Message.Type == MessageType.TextMessage)
                    {
                        Debug.WriteLine(update.Message.Text);
                        botApi.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
                        var response = executor.GetResponse(update.Message.Text);
                        sender.Send(botApi, update.Message.Chat.Id, response);
                    }
                    Offset = update.Id + 1;
                }
            }
        }
    }

}
