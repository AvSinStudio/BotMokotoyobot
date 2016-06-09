using System;
using System.Collections.Generic;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot
{
    internal class TelegramBot : Bot<Update>
    {
        private readonly Executor bgWorker;
        private readonly Api botApi;

        public TelegramBot(string token, Executor executor, Executor backgroungWorker = null)
        {
            bgWorker = backgroungWorker;
            botApi = new Api(token);
            Executor = executor;
            var me = botApi.GetMe();
            Console.WriteLine($"Hello, I'm {me.Result.Username}");
            Offset = botApi.MessageOffset;
        }

        private Executor Executor { get; }
        private int Offset { get; set; }

        public override void SendMessage(Update update, string response)
        {
            botApi.SendTextMessage(update.Message.Chat.Id, response);
        }

        public override IEnumerable<Update> GetUpdates()
        {
            return botApi.GetUpdates(Offset).Result;
        }

        public override void ListenAndAnswer()
        {
            while (true)
            {
                var updates = GetUpdates();
                foreach (var update in updates)
                {
                    if (update.Message.Type == MessageType.TextMessage)
                    {
                        Debug.WriteLine(update.Message.Text);
                        botApi.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
                        var response = Executor.GetResponse(update.Message.Text, update.Message.Chat.Id);
                        botApi.SendTextMessage(update.Message.Chat.Id, response);
                    }

                    Offset = update.Id + 1;
                }
            }
        }
    }
}