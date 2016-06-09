using System;
using System.Collections.Generic;
using System.Configuration;
using PyTaskBot.App.Bot;
using PyTaskBot.App.Commands;
using PyTaskBot.Domain;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var db = new PyTaskDatabase("http://pytask.info/db/db_full.json");
            var dbUpdater = new Scheduler<Task>(TimeSpan.FromSeconds(30), (sender, e) => { Console.WriteLine(); });
            var storage = new Dictionary<long, List<string>>();
            var token = ConfigurationManager.AppSettings.Get("token");
            if (token == null)
            {
                throw new ConfigurationErrorsException("You must specify bot token in configuration file");
            }
            new TelegramBot(token, CreateExecutor(db, storage)).ListenAndAnswer();
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
            executor.Register(new FollowCommand(ptdb, storage, new[] {"фоллоу", "follow", "следить"}));
            return executor;
        }
    }
}