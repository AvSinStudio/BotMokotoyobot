using System.Configuration;
using PyTaskBot.App.Bot;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App
{
    internal class Program
    {
        private static void Main(string[] args)
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