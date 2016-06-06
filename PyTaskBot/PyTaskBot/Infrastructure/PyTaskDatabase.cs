using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PyTaskBot.Domain;

namespace PyTaskBot.Infrastructure
{
    public class PyTaskDatabase:Database
    {
        public PyTaskDatabase(string uri) : base(uri)
        {
        }

        public IEnumerable<string> GetListOfTasks()
        {
            return Db.Keys.OrderBy(x => x, StringComparer.OrdinalIgnoreCase);
        }

        public Task GetInfoAboutTask(string name)
        {
            return Db.FirstOrDefault(x => x.Key == name).Value;
        }

        public IEnumerable<string> GetListOfCategories()
        {
            return Db.Values.Select(x => x.Category).Distinct().OrderBy(x => x, StringComparer.OrdinalIgnoreCase);
        }

        public Task GetTaskWithMax<T>(Func<Task, T> maxFunc)
        {
            return Db.Values.OrderBy(maxFunc).FirstOrDefault();
        }
        public Task GetTaskWithMin<T>(Func<Task, T> maxFunc)
        {
            return Db.Values.OrderByDescending(maxFunc).FirstOrDefault();
        }
        public IEnumerable<string> GetNamesOfTasksInCategory(string category)
        {
            return Db.Where(x => x.Value.Category == category).Select(x => x.Key);
        }
    }
}