using System.Collections.Generic;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot
{
    internal class TelegramBot : Bot<Update>
    {
        private readonly Api botApi;

        private Executor Executor { get; }
        private int Offset { get; set; }

        public TelegramBot(string token, Executor executor)
        {
            botApi = new Api(token);
            Executor = executor;
            Offset = botApi.MessageOffset;
        }

        public string GetWelcome()
        {
            var me = botApi.GetMe();
            return $"Hello, I'm {me.Result.Username}";
        }

        public override void SendMessage(Update update, string response)
        {
            botApi.SendTextMessage(update.Message.Chat.Id, response);
        }

        public override IEnumerable<Update> GetUpdates()
        {
            return botApi.GetUpdates(Offset).Result;
        }

        private static string GetCommandName(string query)
        {
            return query.Split(' ')[0];
        }

        public override void Run()
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

                        var commandName = GetCommandName(update.Message.Text);
                        var response = Executor.Execute(commandName, new object[] { update.Message.Text, update.Message.Chat.Id });

                        botApi.SendTextMessage(update.Message.Chat.Id, response);
                    }

                    Offset = update.Id + 1;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}