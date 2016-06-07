using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using PyTaskBot.App.Bot.Wrappers;
using PyTaskBot.Domain;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class SpecialCommand : Command
    {
        private readonly TaskWrapper taskWrapper;
        private readonly PyTaskDatabase db;

        private const string regex = @"([a-zA-Zа-яёА-ЯЁ\-\., ])?(?<Scope>(минимальн|максимальн)(ый (средний)? (балл|процент)|ое количество сдавших))( в категории (?<Category>[a-zA-Zа-яёА-ЯЁ\-\., ]+))?(\?)?";
        public SpecialCommand(PyTaskDatabase db) : base("min", "command to take task with some param")
        {
            taskWrapper = new TaskWrapper();
            this.db = db;
            Aliases.Add(regex);
        }

        public override string CreateResponse(string[] args)
        {
            var query = args[0];
            var toSearch = db;
            var matches = Regex.Matches(query, regex, RegexOptions.IgnoreCase);
            var question = matches[0].Groups["Scope"].Value;
            var category = matches[0].Groups["Category"].Value;
            var spacePos = question.IndexOf(" ", StringComparison.OrdinalIgnoreCase);
            var isMin = question.Substring(0, spacePos).StartsWith("мин");
            var scope = question.Substring(spacePos + 1).Trim();
            var scopeFunc = GetScope(scope);
            if (query.Contains("в категории"))
            {
                if (db.IsCategory(category))
                    toSearch = db.GetTaskInCategory(category);
                else
                    return "Ошибка в имени категории";
            }
            var task = isMin ? toSearch.GetTaskWithMin(scopeFunc) : toSearch.GetTaskWithMax(scopeFunc);
            return taskWrapper.GetWrapped(task);

        }
        private Func<Task, double> GetScope(string query)
        {
            var d = new Dictionary<string, Func<Task, double>>()
            {
                {"средний балл", task => task.PointsStat.AveragePoint},
                {"средний процент", task => task.PointsStat.AveragePercent},
                {"количество сдавших", task => task.PointsStat.Count}
            };
            return d.FirstOrDefault(x => string.Equals(query, x.Key, StringComparison.OrdinalIgnoreCase)).Value;
        }

        public override bool CheckAliases(string query)
        {
            return Aliases.Any(x => Regex.Match(query, x, RegexOptions.IgnoreCase).Success);
        }
    }
}