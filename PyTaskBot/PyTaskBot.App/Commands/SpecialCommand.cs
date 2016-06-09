using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PyTaskBot.Domain;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Commands
{
    public class SpecialCommand : Command
    {
        private const string Regex =
            @"([a-zA-Zа-яёА-ЯЁ\-\., ])?" +
            @"(?<Scope>(минимальн|максимальн)(ый (средний)? (балл|процент)|ое количество сдавших))" +
            @"( в категории (?<Category>[a-zA-Zа-яёА-ЯЁ\-\., ]+))?(\?)?";

        private readonly PyTaskDatabase db;
        private readonly TaskWrapper taskWrapper;

        public SpecialCommand(PyTaskDatabase db) : base("min", "command to take task with some param")
        {
            taskWrapper = new TaskWrapper();
            this.db = db;
            Aliases.Add(Regex);
        }

        public override string CreateResponse(params object[] args)
        {
            var query = args[0] as string;

            var toSearch = db;

            var matches = System.Text.RegularExpressions.Regex.Matches(query, Regex, RegexOptions.IgnoreCase);
            var question = matches[0].Groups["Scope"].Value;
            var category = matches[0].Groups["Category"].Value;

            var spacePos = question.IndexOf(" ", StringComparison.OrdinalIgnoreCase);
            var isMin = question.Substring(0, spacePos).StartsWith("мин");
            var scope = question.Substring(spacePos + 1).Trim();
            var scopeFunc = GetScope(scope);

            if (query?.Contains("в категории") ?? false)
            {
                if (db.IsCategory(category))
                {
                    toSearch = db.GetTaskInCategory(category);
                }
                else
                {
                    return "Ошибка в имени категории";
                }
            }

            var task = isMin ? toSearch.GetTaskWithMin(scopeFunc) : toSearch.GetTaskWithMax(scopeFunc);
            return taskWrapper.GetWrapped(task);
        }

        private Func<Task, double> GetScope(string query)
        {
            var d = new Dictionary<string, Func<Task, double>>
            {
                {"средний балл", task => task.PointsStat.AveragePoint},
                {"средний процент", task => task.PointsStat.AveragePercent},
                {"количество сдавших", task => task.PointsStat.Count}
            };

            return d.FirstOrDefault(x => query.Equals(x.Key, StringComparison.OrdinalIgnoreCase)).Value;
        }

        public override bool HasAlias(string alias)
        {
            return
                Aliases.Any(x => System.Text.RegularExpressions.Regex.Match(alias, x, RegexOptions.IgnoreCase).Success);
        }
    }
}