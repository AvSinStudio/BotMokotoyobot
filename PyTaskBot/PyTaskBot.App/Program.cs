using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PyTaskBot.Infrastructure;
using Telegram.Bot.Types;
using System.Configuration;
using Newtonsoft.Json;

namespace PyTaskBot.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database();

            var token = ConfigurationManager.AppSettings.Get("token");

            if (token == null)
            {
                throw new ConfigurationErrorsException("You must specify bot token in configuration file");
            }

            var bot = new Telegram.Bot.Api(token);
            var me = bot.GetMe();

            Console.WriteLine($"Hello, I'm {me.Result.Username}");
            
            var offset = bot.MessageOffset;
            while (true)
            {
                var updates = bot.GetUpdates(offset);

                foreach (var update in updates.Result)
                {
                    if (update.Message.Type == MessageType.TextMessage)
                    {
                        Console.WriteLine(update.Message.Text);
                        bot.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
                        if (db.Db.ContainsKey(update.Message.Text))
                        {
                            var task = db.Db[update.Message.Text];

                            var msgBuilder = new StringBuilder();
                            msgBuilder.AppendFormat("Task: {0}\n\n", update.Message.Text);
                            msgBuilder.AppendFormat("Category: {0}\n\n", task.Category);
                            msgBuilder.AppendFormat("The task was taken {0} times\n\n", task.TakenTasks.Count);
                            msgBuilder.AppendFormat("Points stat: {0}", JsonConvert.SerializeObject(task.PointsStat, Formatting.Indented));

                            bot.SendTextMessage(update.Message.Chat.Id, msgBuilder.ToString());
                        }
                    }

                    offset = update.Id + 1;
                }
            }
        }
    }
}