using System;
using System.Diagnostics;
using PyTaskBot.App.Bot.Commands;
using PyTaskBot.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot
{
    internal class TelegramBot
    {
        private readonly Api botApi;
        private readonly Executor executor;

        private int Offset { get; set; }

        public TelegramBot(string token, PyTaskDatabase database)
        {
            botApi = new Api(token);

            executor = new Executor();
            executor.Register(new HelpCommand());
            executor.Register(new ListTaskCommand(database));
            executor.Register(new ListCategoriesCommand(database));
            executor.Register(new TaskInfoCommand(database));
            executor.Register(new ListTaskInCategoryCommand(database));
            executor.Register(new SpecialCommand(database));

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
                        var response = executor.GetResponse(update.Message.Text, update.Message.Chat.Id);
                        botApi.SendTextMessage(update.Message.Chat.Id, response);
                    }

                    Offset = update.Id + 1;
                }
            }
        }
    }
}