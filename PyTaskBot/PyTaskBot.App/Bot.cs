using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PyTaskBot.Infrastructure;
using Telegram.Bot.Types;

namespace PyTaskBot.App
{
    class Bot
    {
        private readonly Telegram.Bot.Api BotApi;
        private readonly Database Database;
        private int Offset { get; set; }
        public Bot(string token, Database database)
        {
            BotApi = new Telegram.Bot.Api(token);
            Database = database;
            var me = BotApi.GetMe();

            Console.WriteLine($"Hello, I'm {me.Result.Username}");

            Offset = BotApi.MessageOffset;
        }

        public void MainLoop()
        {
            while (true)
            {
                var updates = BotApi.GetUpdates(Offset);

                foreach (var update in updates.Result)
                {
                    if (update.Message.Type == MessageType.TextMessage)
                    {
                        Console.WriteLine(update.Message.Text);
                        BotApi.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
                        if (update.Message.Text == "/list")
                            BotApi.SendTextMessage(update.Message.Chat.Id, string.Join("\n", Database.Db.Keys));
                        else if (Database.Db.ContainsKey(update.Message.Text))
                        {
                            var task = Database.Db[update.Message.Text];

                            var msgBuilder = new StringBuilder();
                            msgBuilder.AppendFormat("Task: {0}\n\n", update.Message.Text);
                            msgBuilder.AppendFormat("Category: {0}\n\n", task.Category);
                            msgBuilder.AppendFormat("The task was taken {0} times\n\n", task.TakenTasks.Count);
                            msgBuilder.AppendFormat("Points stat: {0}",
                                JsonConvert.SerializeObject(task.PointsStat, Formatting.Indented));

                            BotApi.SendTextMessage(update.Message.Chat.Id, msgBuilder.ToString());
                        }
                        else
                        {
                            BotApi.SendTextMessage(update.Message.Chat.Id, "This task doesn't exists");
                        }
                    }

                    Offset = update.Id + 1;
                }
            }
        }
    }
}
