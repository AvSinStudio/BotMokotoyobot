using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PyTaskBot.Infrastructure;
using Telegram.Bot.Types;
using System.Configuration;
using Newtonsoft.Json;
using PyTaskBot.App.Bot;

namespace PyTaskBot.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new PyTaskDatabase("http://pytask.info/db/db_full.json");
            var token = ConfigurationManager.AppSettings.Get("token");
            if (token == null)
            {
                throw new ConfigurationErrorsException("You must specify bot token in configuration file");
            }
            new TelegramBot(token, db).MainLoop();
        }
    }
}