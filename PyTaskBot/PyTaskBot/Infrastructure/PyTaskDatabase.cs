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

        public IEnumerable<string> GetTasks()
        {
            return Db.Keys.OrderBy(x => x, StringComparer.OrdinalIgnoreCase);
        }

        public Task GetInfoAboutTask(string name)
        {
            return Db.FirstOrDefault(x => string.Equals(x.Key, name, StringComparison.OrdinalIgnoreCase)).Value;
        }

        public IEnumerable<string> GetCategories()
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
            return Db.Where(x => string.Equals(x.Value.Category, category, StringComparison.OrdinalIgnoreCase)).Select(x => x.Key);
        }

        public bool IsTask(string query)
        {
            return Db.Any(x => x.Key == query);
        }
        public bool IsCategory(string query)
        {
            return Db.Any(x => x.Value.Category == query);
        }

    }
}