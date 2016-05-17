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
            new Bot(token, db).MainLoop();
        }
    }
}