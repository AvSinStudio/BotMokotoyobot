using System;
using System.Collections.Generic;
using System.Configuration;
using System.Timers;
using PyTaskBot.App.Bot;
using PyTaskBot.App.Commands;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App
{
    internal static class Program
    {
        private static Scheduler CreateScheduler()
        {
            var updateInterval = TimeSpan.FromSeconds(30);
            ElapsedEventHandler handler = (sender, e) => { Console.WriteLine(); };
            return new Scheduler(updateInterval, handler);
        }

        private static string GetSetting(string name)
        {
            var setting = ConfigurationManager.AppSettings.Get(name);
            if (setting == null)
            {
                throw new ConfigurationErrorsException($"You must specify ${name} in configuration file");
            }
            return setting;
        }

        private static Executor CreateExecutor(PyTaskDatabase ptdb, Dictionary<long, List<string>> storage)
        {
            var executor = new Executor();
            executor.Register(new HelpCommand());
            executor.Register(new ListTaskCommand(ptdb));
            executor.Register(new ListCategoriesCommand(ptdb));
            executor.Register(new TaskInfoCommand(ptdb));
            executor.Register(new ListTaskInCategoryCommand(ptdb));
            executor.Register(new SpecialCommand(ptdb));
            executor.Register(new FollowCommand(ptdb, storage));
            return executor;
        }

        private static void Main(string[] args)
        {
            var scheduler = CreateScheduler();

            var token = GetSetting("token");

            var dbUrl = GetSetting("dbURL");
            var db = new PyTaskDatabase(dbUrl);
            var storage = new Dictionary<long, List<string>>();
            var executor = CreateExecutor(db, storage);

            var tgBot = new TelegramBot(token, executor);
            Console.WriteLine(tgBot.GetWelcome());
            tgBot.Run();
        }
    }
}