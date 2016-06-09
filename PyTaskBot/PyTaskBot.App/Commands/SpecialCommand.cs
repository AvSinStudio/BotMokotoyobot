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
        private const string PhraseRegex =
            @"([a-zA-Zа-яёА-ЯЁ\-\., ])?" +
            @"(?<Scope>(минимальн|максимальн)(ый (средний)? (балл|процент)|ое количество сдавших))" +
            @"( в категории (?<Category>[a-zA-Zа-яёА-ЯЁ\-\., ]+))?(\?)?";

        private readonly PyTaskDatabase db;

        public SpecialCommand(PyTaskDatabase db)
            : base(new [] { "min" }, "command to take task with some param")
        {
            this.db = db;
            Names.Add(PhraseRegex);
        }

        public override string CreateResponse(object[] args)
        {
            var query = args[0] as string;
            if (query == null) throw new ArgumentException("Argument must be string");

            var toSearch = db;

            var matches = Regex.Matches(query, PhraseRegex, RegexOptions.IgnoreCase);
            var question = matches[0].Groups["Scope"].Value;
            var category = matches[0].Groups["Category"].Value;

            var spacePos = question.IndexOf(" ", StringComparison.OrdinalIgnoreCase);
            var isMin = question.Substring(0, spacePos).StartsWith("мин");
            var scope = question.Substring(spacePos + 1).Trim();
            var scopeFunc = GetScope(scope);

            if (query.Contains("в категории"))
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
            return task.ToString();
        }

        private static Func<Task, double> GetScope(string query)
        {
            var d = new Dictionary<string, Func<Task, double>>
            {
                {"средний балл", task => task.PointsStat.AveragePoint},
                {"средний процент", task => task.PointsStat.AveragePercent},
                {"количество сдавших", task => task.PointsStat.Count}
            };

            return d.FirstOrDefault(x => query.Equals(x.Key, StringComparison.OrdinalIgnoreCase)).Value;
        }

        public override bool CanBeCalledBy(string name)
        {
            return Names.Any(x => Regex.Match(name, x, RegexOptions.IgnoreCase).Success);
        }
    }
}